using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;


namespace WebApplication1.Entities
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var mock = new List<Doctor>();
            mock.Add(new Doctor { IdDoctor = 1, FirstName = "Stefan", LastName = "Stefanski", Email = "stefanstefanski1@gmail.com" });
            mock.Add(new Doctor { IdDoctor = 2, FirstName = "Michal", LastName = "Michalski", Email = "michalmichalski@gmail.com" });

            modelBuilder.Entity<Doctor>()
                     .HasData(mock);


            var mock2 = new List<Patient>();
            mock2.Add(new Patient { IdPatient = 1, FirstName = "Karol1", LastName = "Karolak1", Birthdate = new DateTime(1998, 7, 20) });
            mock2.Add(new Patient { IdPatient = 2, FirstName = "Karol2", LastName = "Karolak2", Birthdate = new DateTime(1998, 7, 24) });

            modelBuilder.Entity<Patient>()
                     .HasData(mock2);

            var mock3 = new List<Prescription>();
            mock3.Add(new Prescription { IdPrescription = 1, Date = new DateTime(2010, 1, 21), DueDate = new DateTime(2020, 1, 21), IdPatient = 1, IdDoctor = 2 });
            mock3.Add(new Prescription { IdPrescription = 2, Date = new DateTime(2009, 10, 1), DueDate = new DateTime(2019, 10, 1), IdPatient = 2, IdDoctor = 2 });
            mock3.Add(new Prescription { IdPrescription = 3, Date = new DateTime(2010, 2, 21), DueDate = new DateTime(2030, 2, 21), IdPatient = 1, IdDoctor = 1 });

            modelBuilder.Entity<Prescription>()
                    .HasData(mock3);

            var mock4 = new List<Medicament>();
            mock4.Add(new Medicament { IdMedicament = 1, Name = "Beladonna", Description = "for sore throat", Type = "syroup" });
            mock4.Add(new Medicament { IdMedicament = 2, Name = "Apap", Description = "for backache", Type = "pill" });
            mock4.Add(new Medicament { IdMedicament = 3, Name = "Ibuprom", Description = "for headache", Type = "pill" });

            modelBuilder.Entity<Medicament>()
                    .HasData(mock4);

            var mock5 = new List<Prescription_Medicament>();
            mock5.Add(new Prescription_Medicament { IdMedicament = 1, IdPrescription = 1, Dose = 1, Details = "stop after 5 days" });
            mock5.Add(new Prescription_Medicament { IdMedicament = 2, IdPrescription = 1, Dose = 2, Details = "stop after 5 days" });
            mock5.Add(new Prescription_Medicament { IdMedicament = 3, IdPrescription = 1, Dose = 3, Details = "stop after 5 days" });
            mock5.Add(new Prescription_Medicament { IdMedicament = 3, IdPrescription = 2, Dose = 3, Details = "stop after 5 days" });
            mock5.Add(new Prescription_Medicament { IdMedicament = 1, IdPrescription = 3, Dose = 1, Details = "stop after 5 days" });

            modelBuilder.Entity<Prescription_Medicament>()
                   .HasData(mock5);

        }
    }
 }

