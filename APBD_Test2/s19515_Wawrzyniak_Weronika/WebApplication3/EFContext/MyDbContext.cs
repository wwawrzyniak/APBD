using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Entities;

namespace WebApplication3.EFContext
{
    public class MyDbContext : DbContext
    {
        public DbSet<Volunteer> Volunteers { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<BreedType> BreedTypes { get; set; }
        public DbSet<Volunteer_Pet> Volunteer_Pets { get; set; }


        public MyDbContext()
        {

        }

        public MyDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Volunteer>(entity =>
            {
                entity.HasKey(e => e.IdVolunteer);
                entity.Property(e => e.IdVolunteer).ValueGeneratedOnAdd();
                entity.Property(e => e.Name).HasMaxLength(80);
                entity.Property(e => e.Surname).HasMaxLength(80);
                entity.Property(e => e.Phone).HasMaxLength(30);
                entity.Property(e => e.Address).HasMaxLength(100);
                entity.Property(e => e.Email).HasMaxLength(80);

                entity.ToTable("Volunteer");

                entity.HasMany(d => d.Volunteer_Pet)
                       .WithOne(p => p.Volunteer)
                       .HasForeignKey(p => p.IdVolunteer)
                       .IsRequired();


                entity.HasMany(d => d.Volunteers)
                       .WithOne(p => p.Supervisor)
                       .HasForeignKey(p => p.IdSupervisor)
                       .OnDelete(DeleteBehavior.NoAction)
                       .IsRequired();



            });

            modelBuilder.Entity<Volunteer_Pet>(entity =>
            {
                entity.HasKey(e => new { e.IdVolunteer, e.IdPet });
                entity.ToTable("Volunteer_Pet");


            });

            modelBuilder.Entity<Pet>(entity =>
            {
                entity.HasKey(e => e.IdPet);
                entity.Property(e => e.IdPet).ValueGeneratedOnAdd();
                entity.Property(e => e.Name).HasMaxLength(80);
                entity.ToTable("Pet");

                entity.HasMany(d => d.Volunteer_Pet)
                       .WithOne(p => p.Pet)
                       .HasForeignKey(p => p.IdPet)
                       .IsRequired();



            });


            modelBuilder.Entity<BreedType>(entity =>
            {
                entity.HasKey(e => e.IdBreedType);
                entity.Property(e => e.IdBreedType).ValueGeneratedOnAdd();
                entity.Property(e => e.Name).HasMaxLength(50);
                entity.Property(e => e.Description).HasMaxLength(500);

                entity.ToTable("BreedType");

                entity.HasMany(d => d.Pet)
                       .WithOne(p => p.BreedType)
                       .HasForeignKey(p => p.IdBreedType)
                       .IsRequired();
            });

        }
    }
}
