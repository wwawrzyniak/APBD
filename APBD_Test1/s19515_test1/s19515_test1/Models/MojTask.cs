using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace s19515_test1.Models
{
    public class MojTask
    {
        public int IdTask { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }

        public string TaskType { get; set; }

        public string ProjectName { get; set; }
    }
}
