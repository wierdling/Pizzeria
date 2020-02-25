using System.Collections.Generic;
using PizzeriaWebApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace PizzeriaWebApi.Repositories
{
    public class PizzaWebRepository: WebRepositoryBase, IPizzaWebRepository
    {
        
        public PizzaWebRepository(IConfiguration configuration, ILogger<WebRepositoryBase> logger): base(configuration)
        {
            Logger = logger;
            Initialize("PizzasServiceUrl");
        }

        public List<PizzaModel> LoadPizzas()
        {
            return Get<List<PizzaModel>>("pizzas");
        }

        public PizzaModel LoadPizza(int id)
        {
            return Get<PizzaModel>($"pizzas/{id}");
        }

        public List<ToppingsModel> LoadToppings()
        {
            return Get<List<ToppingsModel>>("pizzas.json");
        }
    }
}
