using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using PizzeriaWebApi.Models;

namespace PizzeriaWebApi.Repositories
{
    public class MongoRepositoryBase
    {
        private string ConnectionString { get; set; }
        private IConfiguration Configuration { get; set; }
        protected ILogger Logger { get; set; }

        public MongoRepositoryBase(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void Initialize(string urlSettingName)
        {
            ConnectionString = Configuration.GetValue<string>(urlSettingName);
            if (string.IsNullOrEmpty(ConnectionString))
            {
                string message = $"Could not load connection string from config for config name ${urlSettingName}.";
                Logger.LogError(message);
                throw new ArgumentException(message);
            }
        }

        public async Task<List<T>> GetAllItemsInCollection<T>(string databaseName, string collectionName)
        {
            var returnList = await GetDatabase(databaseName).GetCollection<T>(collectionName).Find<T>(t => true).ToListAsync(); ;
            return returnList;
        }

        public T GetItemById<T>(string databaseName, string collectionName, string id)
        {
            var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
            var entity = GetDatabase(databaseName).GetCollection<T>(collectionName).Find(filter).FirstOrDefault();
            return entity;
        }

        public void Insert<T>(string databaseName, string collectionName, T item)
        {
            var collection = GetDatabase(databaseName).GetCollection<T>(collectionName);
            collection.InsertOne(item);
        }

        public async void Update<T>(string databaseName, string collectionName, T item)
        {
            string id = (item as MongoBase).Id;
            var filter = Builders<T>.Filter.Eq(s => (s as MongoBase).Id, id);
            await GetDatabase(databaseName).GetCollection<T>(collectionName).ReplaceOneAsync(filter, item);
        }

        public List<T> GetItemBySearchCriteria<T>(string databaseName, string collectionName, List<KeyValuePair<string, BsonValue>> searchCriteria)
        {
            var finalFilter = new BsonDocument();
            foreach (KeyValuePair<string, BsonValue> kvp in searchCriteria)
            {
                var filter = new BsonDocument(kvp.Key, kvp.Value);
                finalFilter = finalFilter.Merge(filter);
            }

            var returnList = GetDatabase(databaseName).GetCollection<T>(collectionName).Find(finalFilter).ToList();
            return returnList;
        }

        private IMongoDatabase GetDatabase(string databaseName)
        {
            var client = new MongoDB.Driver.MongoClient(ConnectionString);
            var db = client.GetDatabase(databaseName);
            return db;
        }


    }
}
