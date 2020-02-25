using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace PizzeriaWebApi.Models
{
    public class OrdersModel: MongoBase
    {
        [JsonProperty("pizzas")]
        [BsonIgnore]
        public List<PizzaModel> Pizzas { get; set; }

        [JsonIgnore]
        [BsonElement("pizzas")]
        public List<string> PizzaIds { get; set; }

        [JsonProperty("orderId")]
        [BsonElement("orderId")]
        public string OrderId { get; set; }

        [JsonProperty("orderDate")]
        [BsonElement("orderDate")]
        public DateTime OrderDate { get; set; }

        public OrdersModel()
        {
            Pizzas = new List<PizzaModel>();
        }
    }
}
