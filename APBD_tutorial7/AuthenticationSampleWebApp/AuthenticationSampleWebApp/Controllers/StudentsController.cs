using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AuthenticationSampleWebApp.DTOs;
using AuthenticationSampleWebApp.DTOs.Requests;
using AuthenticationSampleWebApp.Models;
using AuthenticationSampleWebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AuthenticationSampleWebApp.Controllers
{

    [ApiController]

    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private IStudentDbService _service;
        public IConfiguration Configuration { get; set; }

        public StudentsController(IConfiguration configuration, IStudentDbService service)
        {
            Configuration = configuration;
            _service=service;
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            var list = new List<Student>();
            list.Add(new Student
            {
                IdStudent = "s1",
                FirstName = "Jan",
                LastName = "Kowalski"
            });
            list.Add(new Student
            {
                IdStudent = "s2",
                FirstName = "Andrzej",
                LastName = "Malewski"
            });

            return Ok(list);
        }


       /* [HttpPost]
        public IActionResult SecureMyPassword(passwordRequest request)
        {
           return  _service.SecureMyPassword(request);
        }*/

        [HttpPost]
          public IActionResult Login(LoginRequestDto request)
          {
        
           var result = _service.LoginCheck(request);
            var okResult = result as OkObjectResult;
            Console.Write(okResult);

            if (result.Equals(okResult))
            {

                var claims = new[]
                  {
                new Claim(ClaimTypes.NameIdentifier, request.Login),
                new Claim(ClaimTypes.Role, "admin")
            };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecretKey"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken
                (
                    issuer: "Gakko",
                    audience: "Students",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(10),
                    signingCredentials: creds
                );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    refreshToken = Guid.NewGuid() //saved to the db
                });
            }
            return StatusCode(401);
        }

        }

    }

