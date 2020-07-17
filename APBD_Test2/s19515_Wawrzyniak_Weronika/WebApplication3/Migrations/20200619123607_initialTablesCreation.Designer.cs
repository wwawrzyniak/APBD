﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication3.EFContext;

namespace WebApplication3.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20200619123607_initialTablesCreation")]
    partial class initialTablesCreation
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebApplication3.Entities.BreedType", b =>
                {
                    b.Property<int>("IdBreedType")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("IdBreedType");

                    b.ToTable("BreedType");
                });

            modelBuilder.Entity("WebApplication3.Entities.Pet", b =>
                {
                    b.Property<int>("IdPet")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("ApprocimateDateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateAdopted")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateRegistered")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdBreedType")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(80)")
                        .HasMaxLength(80);

                    b.Property<bool>("isMale")
                        .HasColumnType("bit");

                    b.HasKey("IdPet");

                    b.HasIndex("IdBreedType");

                    b.ToTable("Pet");
                });

            modelBuilder.Entity("WebApplication3.Entities.Volunteer", b =>
                {
                    b.Property<int>("IdVolunteer")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(80)")
                        .HasMaxLength(80);

                    b.Property<int>("IdSupervisor")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(80)")
                        .HasMaxLength(80);

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<DateTime>("StartingDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(80)")
                        .HasMaxLength(80);

                    b.HasKey("IdVolunteer");

                    b.ToTable("Volunteer");
                });

            modelBuilder.Entity("WebApplication3.Entities.Volunteer_Pet", b =>
                {
                    b.Property<int>("IdVolunteer")
                        .HasColumnType("int");

                    b.Property<int>("IdPet")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateAccepted")
                        .HasColumnType("datetime2");

                    b.HasKey("IdVolunteer", "IdPet");

                    b.HasIndex("IdPet");

                    b.ToTable("Volunteer_Pet");
                });

            modelBuilder.Entity("WebApplication3.Entities.Pet", b =>
                {
                    b.HasOne("WebApplication3.Entities.BreedType", "BreedType")
                        .WithMany("Pet")
                        .HasForeignKey("IdBreedType")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApplication3.Entities.Volunteer_Pet", b =>
                {
                    b.HasOne("WebApplication3.Entities.Pet", "Pet")
                        .WithMany("Volunteer_Pet")
                        .HasForeignKey("IdPet")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplication3.Entities.Volunteer", "Volunteer")
                        .WithMany("Volunteer_Pet")
                        .HasForeignKey("IdVolunteer")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
