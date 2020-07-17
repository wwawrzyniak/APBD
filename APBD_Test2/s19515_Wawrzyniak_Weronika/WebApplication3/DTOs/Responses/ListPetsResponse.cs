using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Entities;

namespace WebApplication3.DTOs.Responses
{
    public class ListPetsResponse
    {
        public List<CustomVolunteer> customVolunteers { get; set; }

        public Pet Pet { get; set; }
    }
}
