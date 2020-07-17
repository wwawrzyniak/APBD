using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Entities
{
    public class Pet
    {
        public int IdPet { get; set; }

        public int IdBreedType { get; set; }

        public string Name { get; set; }

        public bool isMale { get; set; }


        public DateTime DateRegistered { get; set; }


        public DateTime ApprocimateDateOfBirth { get; set; }


        public DateTime DateAdopted { get; set; }

        public virtual ICollection<Volunteer_Pet> Volunteer_Pet { get; set; }

        public virtual BreedType BreedType { get; set; }
    }
}
