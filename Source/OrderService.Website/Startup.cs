using System;
using System.IdentityModel.Tokens.Jwt;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using IdentityModel;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderService.DataProvider;
using OrderService.DataProvider.Entities;
using OrderService.Logic;
using OrderService.Website.Auth;
using OrderService.Website.Filters;

namespace OrderService.Website
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString,
                sql => sql.MigrationsAssembly(typeof(ApplicationContext).Assembly.GetName().Name)));

            var o = new DbContextOptionsBuilder<ApplicationContext>();
            o.UseSqlServer(connectionString);

            //using (var ctx = new ApplicationContext(o.Options))
            //{
            //    ctx.Database.Migrate();
            //}

            services.AddIdentity<User, IdentityRole>(opt =>
                {
                    opt.Password = new PasswordOptions
                    {
                        RequireDigit = false,
                        RequiredLength = 3,
                        RequireNonAlphanumeric = false,
                        RequireUppercase = false,
                        RequireLowercase = false,
                        RequiredUniqueChars = 0
                    };
                    opt.User = new UserOptions
                    {
                        RequireUniqueEmail = true
                    };
                })
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();

            services.AddMvc(opt => opt.Filters.Add(new GlobalExeptionFilter()))
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var identityBuilder = services.AddIdentityServer(options => options.IssuerUri = "OrderService")
                .AddInMemoryApiResources(Config.GetApis())
                .AddInMemoryClients(Config.GetClients())
                .AddInMemoryIdentityResources(Config.GetIdentityResources());

            identityBuilder.AddAspNetIdentity<User>().AddDeveloperSigningCredential();

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
            JwtSecurityTokenHandler.DefaultInboundClaimFilter.Clear();

            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.Authority = "https://localhost:55340";
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters.NameClaimType = JwtClaimTypes.Subject;
                    options.TokenValidationParameters.RoleClaimType = JwtClaimTypes.Role;
                    options.TokenValidationParameters.AuthenticationType =
                        IdentityServerAuthenticationDefaults.AuthenticationScheme;

                    options.Audience = "api";
                });

            services.AddCors(options =>
            {
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins("https://localhost:55340")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            var builder = new ContainerBuilder();

            builder.RegisterModule(new DataModule(connectionString));
            builder.RegisterModule<MapperModule>();
            builder.RegisterModule<BusinessModule>();

            builder.Populate(services);
            var container = builder.Build();

            return new AutofacServiceProvider(container);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseCors("default");
            app.UseIdentityServer();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
