using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PizzeriaWebApi.Models;
using PizzeriaWebApi.Repositories;

namespace PizzeriaWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : BaseController
    {
        private IOrdersRepository OrdersRepository { get; }
        private IPizzaMongoRepository PizzaRepository { get; }

        public OrdersController(ILogger<OrdersController> logger, IOrdersRepository ordersRepository,
            IPizzaMongoRepository pizzaRepository)
        {
            Logger = logger;
            OrdersRepository = ordersRepository;
            PizzaRepository = pizzaRepository;
        }

        [HttpGet("{orderId}")]
        public IActionResult LoadOrder(string orderId)
        {
            Logger.LogDebug("Entering LoadOrder");
            try
            {
                var order =  OrdersRepository.LoadOrder(orderId);
                if (null == order)
                {
                    return NotFound();
                }

                order.Pizzas = new List<PizzaModel>(order.PizzaIds.Count);

                foreach (string pizzaId in order.PizzaIds)
                {
                    var pizza = PizzaRepository.LoadPizza(pizzaId);
                    if (null != pizza)
                    {
                        order.Pizzas.Add(pizza);
                    }
                }
                return Ok(order);
            }
            catch (Exception ex)
            {
                LogException(ex);
                return StatusCode(500, "There was an error when trying to load the order.");
            }
        }

        [HttpPost]
        public IActionResult SaveOrder([FromBody] OrdersModel order)
        {
            Logger.LogDebug("Entering SaveOrder");
            try
            {
                order.PizzaIds = new List<string>(order.Pizzas.Count);
                foreach (var pizza in order.Pizzas)
                {
                    PizzaRepository.SavePizza(pizza);
                    order.PizzaIds.Add(pizza.Id);
                }
                OrdersRepository.SaveOrder(order);
                return Created($"api/orders/{order.Id}", order);
            }
            catch (Exception ex)
            {
                LogException(ex);
                return StatusCode(500, "There was an error when trying to save the order.");
            }
        }
    }
}