using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace PizzeriaWebApi.Models
{
    public class PizzaModel: MongoBase
    {
        [JsonProperty("toppings")]
        [BsonElement("toppings")]
        public List<string> Toppings { get; set; }

        [JsonProperty("size")]
        [BsonElement("size")]
        public string Size { get; set; }

        [JsonProperty("crustType")]
        [BsonElement("crustType")]
        public string CrustType { get; set; }
    }
}
