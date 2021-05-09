using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization;
using Gallery_Flare.Models;

namespace Gallery_Flare.Controllers.Operations.Database
{
    public class DatabaseUserConnector : DatabaseParent
    {
        public DatabaseUserConnector()
        {
            collectionName = "user";
            client = new MongoClient(connectionString);
            database = client.GetDatabase(databaseName);
        }

        public async Task PostUser(string username, string password)
        {
            IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>(collectionName);
            var command = new BsonDocument { { "username", username }, { "password", password } };
            await collection.InsertOneAsync(command);
        }

        public async Task<UserModel> GetUser(string username)
        {
            var builder = Builders<BsonDocument>.Filter;
            var usernameFilter = builder.Eq("username", username);

            IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>(collectionName);
            var document = await collection.Find(usernameFilter).FirstOrDefaultAsync();
            return document != null ? BsonSerializer.Deserialize<UserModel>(document) : null;
        }
    }
}
