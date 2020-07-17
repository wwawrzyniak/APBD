using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamplaryTestEF.Entities
{
    public class Customer
    {
        public int IdClient { get; set; }

        public string Name { get; set; }

        public string Surnam { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
