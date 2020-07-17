using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DTO;

namespace WebApplication1.Service
{
    public interface IDbAnimals
    {
        public IActionResult AddAnimal(AnimlaRequest request);
        public IActionResult getAnimals(string orderBy);
    }
}
