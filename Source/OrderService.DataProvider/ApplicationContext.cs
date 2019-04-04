using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OrderService.Model.Entities;

namespace OrderService.DataProvider
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }

        public DbSet<WorkType> WorkTypes { get; set; }

        public DbSet<Photo> Photos { get; set; }

        public DbSet<Executor> Executors { get; set; }

        public DbSet<ExecutorRequest> ExecutorRequests { get; set; }

        public DbSet<CustomerRequest> CustomerRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>().ToTable("Orders");
            modelBuilder.Entity<Executor>().ToTable("Executors");
            modelBuilder.Entity<WorkType>().ToTable("WorkTypes");
            modelBuilder.Entity<Photo>().ToTable("Photos");
            modelBuilder.Entity<ExecutorRequest>().ToTable("ExecutorRequests");
            modelBuilder.Entity<CustomerRequest>().ToTable("CustomerRequests");

            var orderEntity = modelBuilder.Entity<Order>();
            orderEntity
                .HasMany(o => o.Photos)
                .WithOne(p => p.Order)
                .HasForeignKey(o => o.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            orderEntity
                .HasOne(o => o.Customer)
                .WithMany(p => p.Orders)
                .HasForeignKey(o => o.CustomerUserId)
                .OnDelete(DeleteBehavior.Cascade);

            orderEntity
                .HasMany(x => x.ExecutorRequests)
                .WithOne(x => x.Order)
                .HasForeignKey(x => x.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            orderEntity
                .HasMany(x => x.CustomerRequests)
                .WithOne(x => x.Order)
                .HasForeignKey(x => x.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            var workTypeEntity = modelBuilder.Entity<WorkType>();
            workTypeEntity.HasIndex(x => x.Name).IsUnique();
            workTypeEntity
                .HasMany(x => x.Orders)
                .WithOne(x => x.WorkType)
                .HasForeignKey(x => x.WorkTypeId)
                .OnDelete(DeleteBehavior.SetNull);

            workTypeEntity
                .HasMany(x => x.Executors)
                .WithOne(x => x.WorkType)
                .HasForeignKey(x => x.WorkTypeId)
                .OnDelete(DeleteBehavior.SetNull);

            var executorEntity = modelBuilder.Entity<Executor>();
           executorEntity
                .HasMany(x => x.Orders)
                .WithOne(x => x.Executor)
                .HasForeignKey(x => x.ExecutorId)
                .OnDelete(DeleteBehavior.SetNull);

            executorEntity
                .HasMany(x => x.Photos)
                .WithOne(x => x.Executor)
                .HasForeignKey(x => x.ExecutorId)
                .OnDelete(DeleteBehavior.Cascade);

            executorEntity
                .HasMany(x => x.ExecutorRequests)
                .WithOne(x => x.Executor)
                .HasForeignKey(x => x.ExecutorId)
                .OnDelete(DeleteBehavior.Cascade);

            executorEntity
                .HasMany(x => x.CustomerRequests)
                .WithOne(x => x.Executor)
                .HasForeignKey(x => x.ExecutorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<User>()
                .HasOne(u => u.Executor)
                .WithOne(e => e.User)
                .HasForeignKey<Executor>(u => u.UserId);
        }
    }
}

