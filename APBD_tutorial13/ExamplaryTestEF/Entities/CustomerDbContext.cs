using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamplaryTestEF.Entities
{
        public class CustomerDbContext : DbContext
        {
            public DbSet<Employee> Employees { get; set; }
            public DbSet<Customer> Customers { get; set; }
            public DbSet<Order> Orders { get; set; }
            public DbSet<Confectionery> Confectioneries{ get; set; }

            public DbSet<Confectionery_Order> Confectionery_Orders { get; set; }
            public CustomerDbContext()
            {

            }

            public CustomerDbContext(DbContextOptions options) : base(options)
            {

            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                modelBuilder.Entity<Customer>(entity =>
                {
                    entity.HasKey(e => e.IdClient);
                    entity.Property(e => e.IdClient).ValueGeneratedOnAdd();
                    entity.Property(e => e.Surnam).IsRequired();

                    entity.ToTable("Customer");

                    entity.HasMany(d => d.Orders)
                           .WithOne(p => p.Customer)
                           .HasForeignKey(p => p.IdClient)
                           .IsRequired();
                });

                modelBuilder.Entity<Order>(entity =>
                {
                    entity.HasKey(e => e.IdOrder);
                    entity.Property(e => e.IdOrder).ValueGeneratedOnAdd();
                    entity.ToTable("Order");

                    entity.HasMany(d => d.Confectionery_Order)
                           .WithOne(p => p.Order)
                           .HasForeignKey(p => p.IdOrder)
                           .IsRequired();

                });

                modelBuilder.Entity<Employee>(entity =>
                {
                    entity.HasKey(e => e.IdEmpl);
                    entity.Property(e => e.IdEmpl).ValueGeneratedOnAdd();
                    entity.Property(e => e.Surna).IsRequired();
                    entity.ToTable("Employee");

                    entity.HasMany(d => d.Orders)
                           .WithOne(p => p.Employee)
                           .HasForeignKey(p => p.IdEmployee)
                           .IsRequired();

                });

                modelBuilder.Entity<Confectionery>(entity =>
                {
                    entity.HasKey(e => e.IdConfecti);
                    entity.Property(e => e.IdConfecti).ValueGeneratedOnAdd();
                    entity.Property(e => e.Name).IsRequired();
                    entity.ToTable("Confectionery");

                    entity.HasMany(d => d.Confectionery_Order)
                           .WithOne(p => p.Confectionery)
                           .HasForeignKey(p => p.IdConfection)
                           .IsRequired();


                });

                modelBuilder.Entity<Confectionery_Order>(entity =>
                {
                    entity.HasKey(e => new { e.IdConfection, e.IdOrder });
                    entity.Property(e => e.Quantity).IsRequired();
                    entity.ToTable("Confectionery_Order");


                });


            }
        }
}
