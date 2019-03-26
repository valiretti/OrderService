using System;
using System.IdentityModel.Tokens.Jwt;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using IdentityServer4.AccessTokenValidation;
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
using OrderService.Website.Auth;

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
            string userConnectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(userConnectionString,
                sql => sql.MigrationsAssembly(typeof(ApplicationContext).Assembly.GetName().Name)));

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

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var identityBuilder = services.AddIdentityServer()
                .AddInMemoryApiResources(Config.GetApis())
                .AddInMemoryClients(Config.GetClients());

            identityBuilder.AddAspNetIdentity<User>().AddDeveloperSigningCredential();

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddAuthentication(opt =>
            {
                opt.DefaultScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                opt.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
            });

            var builder = new ContainerBuilder();

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
