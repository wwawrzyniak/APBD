using ExamplaryTestEF.Entities;
using ExamplaryTestEF.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamplaryTestEF.Service
{
    public class DbService : ControllerBase, IDbService
    {
        public IActionResult addOrder(CustomerDbContext _context, AddOrderRequest request, int _clientId)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {

                try
                {
                    var cakesNotInDatabase = 0;
                    //check if products from the request exist in the database
                    request.Confectionery.ForEach(x =>
                    {
                        if (_context.Confectioneries.Where(y => y.Name == x.Name).FirstOrDefault() == null)
                        {
                            cakesNotInDatabase++;
                        }
                    });


                    //If a product has been provided that is not in the database, stop processing the request and return the appropriate error code
                    if (cakesNotInDatabase!=0)
                    {
                        return NotFound("One of the confectionaries doesnt exist in the database");
                    }

                    var nextIdOrder = _context.Orders.Max(o => o.IdOrder) + 1;

                    Order o = new Order
                    {
                        IdClient = _clientId,
                        IdOrder = nextIdOrder,
                        DateAccepted = request.DateAccepted,
                        DateFinished = DateTime.Now,
                        Notes = request.Notes,
                    };
                    _context.Orders.Add(o);

                    var result = request.Confectionery.Select(conf => new Confectionery_Order
                    {
                        IdConfection = _context.Confectioneries.Where(dbConf => dbConf.Name.Equals(conf.Name)).Select(dbConf => dbConf.IdConfecti).ToList().First(),
                        IdOrder = nextIdOrder,
                        Quantity = conf.Quantity,
                        Notes = conf.Notes
                    });

                    foreach (Confectionery_Order co in result)
                    {
                        _context.Confectionery_Orders.Add(co);
                    }

                    _context.SaveChanges();
                    transaction.Commit();
                    return Ok("Created a new order");
                }

                catch (Exception e)
                {
                    transaction.Rollback();
                    return BadRequest(e);
                }
            }
        }
    


        public IActionResult getCustomerOrders(CustomerDbContext _context)
        {
            try
            {
                //The result list does not have to contain information about the employee who was responsible for accepting the order or the customer`s personal data.
                //Instead, it must consider what was included in the order (which confectionery was selected for it). 

                var allOrders = _context.Orders.Select(o => new getCustomerOrders
                                                {
                                                    IdOrder = o.IdOrder,
                                                    IdCustomer = o.IdClient,
                                                    DateAccepted = o.DateAccepted,
                                                    DateFinished = o.DateFinished,
                                                    Notes = o.Notes,
                                                    Confectis = _context.Confectionery_Orders
                                                                              .Where(co => co.IdOrder == o.IdOrder)
                                                                              .Select(co => co.IdConfection).ToList()

                                                })
                                                .ToList();
           
                return Ok(allOrders);
            }
            catch (Exception e) { return BadRequest(e); }
        }

        public IActionResult getCustomerOrders(CustomerDbContext _context, string _customerName)
        {
            try
            {

                //Design an endpoint that will return a list of orders placed by a customer with the given name.
                var customerId = _context.Customers.Where(c => c.Name == _customerName).Select(c => c.IdClient);

                if (customerId == null) return BadRequest("No such customer");

                var res = _context.Orders
                         .Where(d2 => d2.IdClient.Equals(customerId))
                         .ToList();

                //The result list does not have to contain information about the employee who was responsible for accepting the order or the customer`s personal data. Instead, it must consider what was included in the order (which confectionery was selected for it). 
                var res2 = res.Select(o => new getCustomerOrders
                {
                    IdOrder = o.IdOrder,
                    IdCustomer = o.IdClient,
                    DateAccepted = o.DateAccepted,
                    DateFinished = o.DateFinished,
                    Notes = o.Notes,
                    Confectis = _context.Confectionery_Orders
                                              .Where(co => co.IdOrder == o.IdOrder)
                                              .Select(co => co.IdConfection).ToList()

                });
                return Ok(res2);

            }
            catch (Exception e) { return BadRequest(e); }
        }
    }
}
