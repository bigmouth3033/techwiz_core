﻿using Microsoft.EntityFrameworkCore;
using TechWizWebApp.Domain;

namespace TechWizWebApp.Data
{
    public class TechwizDbContext : DbContext
    {
        public TechwizDbContext(DbContextOptions<TechwizDbContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }

        public DbSet<User> Users{ get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Permission> Permissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<User>(options =>
            {
                options.HasMany(u => u.Messages).WithOne(m => m.Customer).HasForeignKey(m => m.CustomerId);

                options.HasOne(u => u.Permission).WithOne(p => p.User).HasForeignKey<Permission>(p => p.EmployeeId);

                options.HasData([
                      new User{
                      Id = 1,
                      Email = "admin@admin.com",
                      Password = "$2a$12$9HwNZ8KCsja/JTshf7.kneBqU.R0+OUQcY5fnoAnAD52.f4ClBAf3i",
                      IsEmailConfirmed = true,
                      FullName = "Admin",
                      Role = "Admin",
                      },
                    ]);

                options.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
                options.Property(e => e.UpdatedAt).HasDefaultValueSql("GETDATE()");
            });

        }
    }
}
