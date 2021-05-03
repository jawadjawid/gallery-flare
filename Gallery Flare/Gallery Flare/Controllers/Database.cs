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
        private string databaseName = "flare";
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

        public async Task<IList<ImageModel>> GetImagesFromDbAsync(string author = "any", string access = "any", string tags = "any")
        {
            var builder = Builders<BsonDocument>.Filter;
            var authorFilter = author == "any" ? builder.Empty : builder.Eq("author", author);
            var accessFilter = access == "any" ? builder.Empty : builder.Eq("access", access);

            IList<ImageModel> results = new List<ImageModel>();
            IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>(collectionName);
            var documents = await collection.Find(authorFilter & accessFilter).ToListAsync();
            foreach (var doc in documents)
                results.Add(BsonSerializer.Deserialize<ImageModel>(doc));

            IList<ImageModel> filtieredResults = new List<ImageModel>();
            if (tags != "any")
            {
                string[] tagsToFind = tags.Split(",");
                foreach (string tagToFind in tagsToFind)
                    foreach (ImageModel result in results)
                        if (result.tags.ToLower().Contains(tagToFind.ToLower()) && !filtieredResults.Contains(result) && tagToFind != "")
                            filtieredResults.Add(result);
            }
            return tags == "any" ? results : filtieredResults;
        }

        public async Task<string> GetIrrelevantTags()
        {
            IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>(collectionName);
            var document = await collection.Find(new BsonDocument()).FirstOrDefaultAsync();
            IrrelevantTagsModel tags = BsonSerializer.Deserialize<IrrelevantTagsModel>(document);
            return tags.words;
        }
    }
}