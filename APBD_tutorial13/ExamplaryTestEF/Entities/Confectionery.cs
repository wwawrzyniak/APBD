using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamplaryTestEF.Entities
{
    public class Confectionery
    {
        public int IdConfecti { get; set; }

        public string Name { get; set; }

        public double PricePerlte { get; set; }

        public string Typ { get; set; }

        public virtual ICollection<Confectionery_Order> Confectionery_Order { get; set; }
    }
   
}
