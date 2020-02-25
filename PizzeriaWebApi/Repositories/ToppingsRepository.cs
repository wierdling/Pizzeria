using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using PizzeriaWebApi.Models;

namespace PizzeriaWebApi.Repositories
{
    public class ToppingsRepository : MongoRepositoryBase, IToppingsRepository
    {
        private const string Collection = "toppings";
        private const string DatabaseName = "pizzas";
        public ToppingsRepository(IConfiguration configuration, ILogger<ToppingsRepository> logger) : base(configuration)
        {
            Logger = logger;
            Initialize("MongoConnectionString");
        }

        public async Task<List<ToppingsModel>> LoadToppings()
        {
            return await GetAllItemsInCollection<ToppingsModel>(DatabaseName, Collection);
        }

        public void AddNewToppingType(ToppingsModel topping)
        {
            Insert(DatabaseName, Collection, topping);
        }

        public ToppingsModel GetByName(string toppingName)
        {
            List<KeyValuePair<string, BsonValue>> searchCriteria = new List<KeyValuePair<string, BsonValue>>
            {
                new KeyValuePair<string, BsonValue>("name", toppingName)
            };
            List<ToppingsModel> toppings =
                GetItemBySearchCriteria<ToppingsModel>(DatabaseName, Collection, searchCriteria);
            if (toppings.Count == 1)
            {
                return toppings[0];
            }

            return null;
        }

        public void UpdateToppingQuantity(string toppingName, decimal quantity)
        {
            var topping = GetByName(toppingName);
            if (null != topping)
            {
                topping.Quantity = quantity;
                Update(DatabaseName, Collection, topping);
            }
            else
            {
                ToppingsModel model = new ToppingsModel {Name = toppingName, Quantity = quantity};
                Insert(DatabaseName, Collection, model);
            }
        }

    }
}
