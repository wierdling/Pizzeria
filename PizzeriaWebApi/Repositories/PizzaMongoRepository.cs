using System.Collections.Generic;
using System.Threading.Tasks;
using PizzeriaWebApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace PizzeriaWebApi.Repositories
{
    public class PizzaMongoRepository: MongoRepositoryBase, IPizzaMongoRepository
    {
        private const string Collection = "pizzas";
        private const string DatabaseName = "pizzas";
        public PizzaMongoRepository(IConfiguration configuration, ILogger<PizzaMongoRepository> logger) : base(configuration)
        {
            Logger = logger;
            Initialize("MongoConnectionString");
        }

        public async Task<List<PizzaModel>> LoadPizzas()
        {
            return await GetAllItemsInCollection<PizzaModel>(DatabaseName, Collection);
        }

        public PizzaModel LoadPizza(string id)
        {
            return GetItemById<PizzaModel>(DatabaseName, Collection, id);
        }

        public void SavePizza(PizzaModel pizza)
        {
            Insert(DatabaseName, Collection, pizza);
        }
    }
}
