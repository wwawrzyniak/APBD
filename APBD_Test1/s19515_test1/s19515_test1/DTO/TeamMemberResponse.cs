using s19515_test1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace s19515_test1.DTO
{
    public class TeamMemberResponse
    {
        public TeamMember teammember { get; set; }

        public List<MojTask> tasksListAssigned { get; set; }

        public List<MojTask> tasksListCreated { get; set; }

    }
}
