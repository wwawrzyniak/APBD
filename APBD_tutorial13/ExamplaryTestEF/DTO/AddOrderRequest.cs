using ExamplaryTestEF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamplaryTestEF.Models
{
    public class AddOrderRequest
    {

        public DateTime DateAccepted { get; set; }


        public string Notes { get; set; }

        public List<CustomConfectionery> Confectionery { get; set;}

    }
}
