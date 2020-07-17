using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebApplication3.Entities
{
    public class Volunteer
    {
        public int IdVolunteer { get; set; }
      //  [ForeignKey("Supervisor")]
        public int IdSupervisor { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }

        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email{ get; set; }

        public DateTime StartingDate { get; set; }

        public virtual ICollection<Volunteer_Pet> Volunteer_Pet { get; set; }
        [JsonIgnore]
        public virtual Volunteer Supervisor { get; set; }
        [JsonIgnore]
        public virtual ICollection<Volunteer> Volunteers { get; set; }




    }
}
