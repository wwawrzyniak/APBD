using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.DTOs.Requests;
using WebApplication3.DTOs.Responses;
using WebApplication3.EFContext;
using WebApplication3.Entities;

namespace WebApplication3.Service
{
    public class PetsDbService : ControllerBase, IDbService
    {

        private readonly MyDbContext _context;

        public PetsDbService(MyDbContext context)
        {
            _context = context;
        }


        public IActionResult listPets(int? year)
        {
            List<ListPetsResponse> finalList = new List<ListPetsResponse>();
            List<Pet> pets = new List<Pet>();
            //If "year" is not provided, the endpoint should return a list of all pets
            
            if(year == null)
            {
                pets = _context.Pets.ToList();
            }
            //return list of pets that have DateRegistered starting with a given year. 
            else 
            {
                pets = _context.Pets.Where(p => p.DateRegistered.Year == year).ToList(); 
            }

            if(pets.Count == 0)
                return BadRequest("No pets");

            //For every pet 
            foreach(Pet p in pets)
                {
                // the list of volunteers should be included 
                    var volunteers = _context.Volunteer_Pets.Where(pet => pet.IdPet == p.IdPet).Select(p => p.IdVolunteer).ToList();

                    List<CustomVolunteer> vcustomVolunteers = new List<CustomVolunteer>();
                //(Name, Surname and phone number).
                    foreach(int x in volunteers)
                    {
                        var VName = _context.Volunteers.Where(v => v.IdVolunteer == x).Select(v => v.Name).ToList().First();
                        var VSurName = _context.Volunteers.Where(v => v.IdVolunteer == x).Select(v => v.Surname).ToList().First();
                        var VPhone = _context.Volunteers.Where(v => v.IdVolunteer == x).Select(v => v.Phone).ToList().First();

                        vcustomVolunteers.Add(new CustomVolunteer { Name = VName, Surname = VSurName, PhoneNumber = VPhone });
                        
                    }
                    finalList.Add(new ListPetsResponse { customVolunteers = vcustomVolunteers, Pet = p });

                //The result should be sorted by DateRegistered in ascending order
                finalList.OrderBy(p => p.Pet.DateRegistered);
                }
            
            return Ok(finalList);
        }

        public IActionResult addPet(AddPetRequest addRequest)
        {

            if(string.IsNullOrEmpty(addRequest.DateRegistered.ToString()) || string.IsNullOrEmpty(addRequest.BreedName.ToString()) || string.IsNullOrEmpty(addRequest.Name.ToString()) || string.IsNullOrEmpty(addRequest.isMale.ToString()) || string.IsNullOrEmpty(addRequest.ApproctimatedDateOfBirth.ToString()))
            {
                return BadRequest("Not enough data");
            }
            var exists = _context.BreedTypes.Where(b => b.Name == addRequest.BreedName).ToList();

            //We should check whether a breed of a given pet exists in the database, if it doesn’t we should add a breed to the database first.

            if(exists.Count == 0)
            {
                _context.BreedTypes.Add(new BreedType { Name = addRequest.BreedName, Description = null });
                _context.SaveChanges();

            }

            var breedId = _context.BreedTypes.Where(b => b.Name == addRequest.BreedName).Select(b => b.IdBreedType).ToList().First();
            _context.Pets.Add(new Pet
            {
                Name = addRequest.Name,
                IdBreedType = breedId,
                isMale = addRequest.isMale,
                DateRegistered = addRequest.DateRegistered,
                ApprocimateDateOfBirth = addRequest.ApproctimatedDateOfBirth
            }) ;

            _context.SaveChanges();

           return Ok(200);
        }
    }
}
