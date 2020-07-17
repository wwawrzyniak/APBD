using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamplaryTestEF.Entities
{
    public class Confectionery_Order
    {

        public int IdConfection { get; set; }

        public int IdOrder { get; set;}

        public int Quantity { get; set; }

        public string Notes { get; set; }

        public virtual Order Order { get; set; }

        public virtual Confectionery Confectionery { get; set; }
    }
}
