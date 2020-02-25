using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PizzeriaWebApi.Models;
using PizzeriaWebApi.Repositories;

namespace PizzeriaWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToppingsController : BaseController
    {
        private IToppingsRepository ToppingsRepository { get; }

        public ToppingsController(ILogger<ToppingsController> logger, IToppingsRepository toppingsRepository)
        {
            Logger = logger;
            ToppingsRepository = toppingsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> LoadToppings()
        {
            Logger.LogDebug("Entering LoadToppings");
            try
            {
                var toppings = await ToppingsRepository.LoadToppings();
                if (null == toppings || toppings.Count == 0)
                {
                    return NotFound();
                }

                return Ok(toppings);
            }
            catch (Exception ex)
            {
                LogException(ex);
                return StatusCode(500, "There was an error when trying to load toppings.");
            }
        }

        [HttpPost]
        public IActionResult SaveTopping([FromBody] ToppingsModel topping)
        {
            Logger.LogDebug("Entering SaveTopping");
            try
            {
                ToppingsRepository.AddNewToppingType(topping);
                return Created($"api/toppings/{topping.Id}", topping);
            }
            catch (Exception ex)
            {
                LogException(ex);
                return StatusCode(500, "There was an error when trying to save the topping.");
            }
        }
    }
}