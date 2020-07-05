using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Services
{
    public interface IDbService
    {
        bool CheckIndexNumber(string index);
    }
}
