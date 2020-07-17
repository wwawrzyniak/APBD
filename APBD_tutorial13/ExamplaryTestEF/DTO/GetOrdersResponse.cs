using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamplaryTestEF
{
    public class getCustomerOrders
    {
        public int IdOrder { get; set; }

        public int IdCustomer { get; set; }
        public DateTime DateAccepted { get; set; }

        public DateTime DateFinished { get; set; }

        public string Notes { get; set; }

        public List<int> Confectis { get; set; }

    }
}
