using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OrderService.DataProvider.Entities;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>().ToTable("Orders");
            modelBuilder.Entity<Executor>().ToTable("Executors");
            modelBuilder.Entity<WorkType>().ToTable("WorkTypes");
            modelBuilder.Entity<Photo>().ToTable("Photos");

            var orderEntity = modelBuilder.Entity<Order>();
            orderEntity
                .HasMany(o => o.Photos)
                .WithOne(p => p.Order)
                .HasForeignKey(o => o.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            var workTypeEntity = modelBuilder.Entity<WorkType>();
            workTypeEntity
                .HasMany(x => x.Orders)
                .WithOne(x => x.WorkType)
                .HasForeignKey(x => x.WorkTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            workTypeEntity
                .HasMany(x => x.Executors)
                .WithOne(x => x.WorkType)
                .HasForeignKey(x => x.WorkTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            var executorEntity = modelBuilder.Entity<Executor>();
            executorEntity.Property(e => e.UserId).IsRequired();
            executorEntity
                .HasMany(x => x.Orders)
                .WithOne(x => x.Executor)
                .HasForeignKey(x => x.ExecutorId)
                .OnDelete(DeleteBehavior.Cascade);

            executorEntity
                .HasMany(x => x.Photos)
                .WithOne(x => x.Executor)
                .HasForeignKey(x => x.ExecutorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

