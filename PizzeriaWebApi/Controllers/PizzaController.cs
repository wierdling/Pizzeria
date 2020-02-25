using System;
using System.Threading.Tasks;
using PizzeriaWebApi.Models;
using PizzeriaWebApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PizzeriaWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : BaseController
    {
        private IPizzaMongoRepository PizzaMongoRepository { get; }

        public PizzaController(ILogger<PizzaController> logger, IPizzaMongoRepository pizzaMongoRepository)
        {
            Logger = logger;
            PizzaMongoRepository = pizzaMongoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            Logger.LogDebug("Loading pizzas.");
            try
            {
                var returnList = await PizzaMongoRepository.LoadPizzas();
                Logger.LogDebug($"Loaded ${returnList.Count} pizzas.");
                return Ok(returnList);
            }
            catch (Exception ex)
            {
                LogException(ex);
                return StatusCode(500, "There was an error when trying to load the pizzas.");
            }
        }

        [HttpPost]
        public IActionResult AddPizzaToMongo([FromBody] PizzaModel pizza)
        {
            Logger.LogDebug("Adding pizza.");
            try
            {
                PizzaMongoRepository.SavePizza(pizza);
                return Created($"mongo/{pizza.Id}", pizza);
            }
            catch (Exception ex)
            {
                LogException(ex);
                return StatusCode(500, "There was an error when trying to save the pizza.");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetPizzaFromMongo(string id)
        {
            Logger.LogDebug($"Loading pizza for id {id}");
            try
            {
                var pizza = PizzaMongoRepository.LoadPizza(id);
                if (null == pizza)
                {
                    return NotFound();
                }

                return Ok(pizza);
            }
            catch (Exception ex)
            {
                LogException(ex);
                return StatusCode(500, $"There was an error when trying to load a pizza for id {id}.");
            }
        }

    }
}
