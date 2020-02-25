using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace PizzeriaWebApi.Models
{
    public class ToppingsModel: MongoBase
    {
        [JsonProperty("name")]
        [BsonElement("name")]
        public string Name { get; set; }

        [JsonProperty("quantity")]
        [BsonElement("quantity")]
        public decimal Quantity { get; set; }
    }
}
