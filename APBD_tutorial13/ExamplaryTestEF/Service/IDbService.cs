using ExamplaryTestEF.Entities;
using ExamplaryTestEF.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamplaryTestEF.Service
{
    public interface IDbService
    {
        IActionResult getCustomerOrders(CustomerDbContext context);
        IActionResult getCustomerOrders(CustomerDbContext context, string customerName);
        IActionResult addOrder(CustomerDbContext context, AddOrderRequest request, int id);
    }
}
