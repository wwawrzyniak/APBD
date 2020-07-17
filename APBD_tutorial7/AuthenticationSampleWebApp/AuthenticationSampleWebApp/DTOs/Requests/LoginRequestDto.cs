using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationSampleWebApp.DTOs.Requests
{
    public class LoginRequestDto
    {
        public string Login { get; set; }
        public string Haslo { get; set; }
    }
}
