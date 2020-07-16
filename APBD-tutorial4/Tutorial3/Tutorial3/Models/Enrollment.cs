using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tutorial3.Models
{

    public class Enrollment
    {
        public int IdEnrollment { get; set; }
        public DateTime StartDate { get; set; }

        public int Semester { get; set; }

        public string studiesName { get; set; }
        
    }
}
