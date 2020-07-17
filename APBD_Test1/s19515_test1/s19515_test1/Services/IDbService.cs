using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace s19515_test1.Services
{
    public interface IDbService
    {
        public IActionResult getTeamMemberAndHisTasks(int id);
        public IActionResult deleteDataAboutProject(int id);
    }
}
