using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PizzeriaWebApi.Models;

namespace PizzeriaWebApi.Repositories
{
    public class OrdersRepository: MongoRepositoryBase, IOrdersRepository
    {
        private const string Collection = "orders";
        private const string DatabaseName = "pizzas";
        
        public OrdersRepository(IConfiguration configuration, ILogger<OrdersRepository> logger) : base(configuration)
        {
            Logger = logger;
            Initialize("MongoConnectionString");
        }

        public OrdersModel LoadOrder(string orderId)
        {
            return GetItemById<OrdersModel>(DatabaseName, Collection, orderId);
        }

        public void SaveOrder(OrdersModel order)
        {
            Insert(DatabaseName, Collection, order);
        }

        public void UpdateOrder(OrdersModel order)
        {
            Update(DatabaseName, Collection, order);
        }
    }
}
