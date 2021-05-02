using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization;
using Gallery_Flare.Models;

namespace Gallery_Flare.Controllers
{
    public class Database
    {
        private MongoClient client;
        private string connectionString = "mongodb+srv://jawad:jawad@cluster0.r6ob1.azure.mongodb.net/flare?retryWrites=true&w=majority";
        private string databaseName =  "flare";
        private IMongoDatabase database;
        private string collectionName;

        public Database(string collectionName)
        {
            this.collectionName = collectionName;
            client = new MongoClient(connectionString);
            database = client.GetDatabase(databaseName);
        }

        public async Task PostToDbAsync(string fileName, string fileURL, string userID, string access, string tags)
        {
            IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>(collectionName);
            var command = new BsonDocument { { "title", fileName }, { "img", fileURL }, { "author", userID }, { "access", access }, { "tags", tags } };
            await collection.InsertOneAsync(command);
        }

        public async Task<IList<ImageModel>> GetFromDbAsync()
        {
            IList<ImageModel> results = new List<ImageModel>();
            IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>(collectionName);
            var documents = await collection.Find(new BsonDocument()).ToListAsync();
            foreach (var doc in documents)
                results.Add(BsonSerializer.Deserialize<ImageModel>(doc));
            return results;
        }


    }
}
