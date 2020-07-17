using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Entities
{
    public class Volunteer_Pet
    {

        public int IdVolunteer { get; set; }
        public int IdPet { get; set; }
        public DateTime DateAccepted { get; set; }

        public virtual Volunteer Volunteer { get; set; }

        public virtual Pet Pet { get; set; }


    }
}
