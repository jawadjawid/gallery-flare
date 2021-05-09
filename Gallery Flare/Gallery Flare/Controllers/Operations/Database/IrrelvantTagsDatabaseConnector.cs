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
    public class IrrelvantTagsDatabaseConnector : DatabaseParent
    {
        public IrrelvantTagsDatabaseConnector()
        {
            collectionName = "words";
            client = new MongoClient(connectionString);
            database = client.GetDatabase(databaseName);
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
