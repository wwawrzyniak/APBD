using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apbd_example_tutorial_10.Models
{
    public class EnrollStudentRequest
    {
        //[RegularExpression("^s[0-9]+$")]
        public string IndexNumber { get; set; }

        public string Email { get; set; }


        public string FirstName { get; set; }

        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }

        public string Studies { get; set; }
    }
}
