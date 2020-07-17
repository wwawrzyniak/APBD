using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.DTOs.Requests;

namespace WebApplication3.Service
{
    public interface IDbService
    {
        IActionResult listPets(int? year);
        IActionResult addPet(AddPetRequest addRequest);
    }
}
