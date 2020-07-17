using ExamplaryTestEF.Entities;
using ExamplaryTestEF.Models;
using ExamplaryTestEF.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamplaryTestEF.Controllers
{
    [Route("api/controllers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerDbContext _context;


        private readonly IDbService _dbService;
        public CustomerController(CustomerDbContext context, IDbService dbService)
        {
            _context = context;
            _dbService = dbService;
        }

       [HttpGet]
        public IActionResult GetOrders()
        {
            return _dbService.getCustomerOrders(_context);
        }


        [HttpGet("{_customerName?}")]
        public IActionResult GetOrders(string _customerName)
        {
            return _dbService.getCustomerOrders(_context, _customerName);
        }


        [HttpPost("client/{clientId}/orders")]
        public IActionResult AddOrder(int _clientId, AddOrderRequest request)
        {
            return _dbService.addOrder(_context, request, _clientId);
        }  
    }
}
    
