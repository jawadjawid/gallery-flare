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
    public class DatabaseParent
    {
        public MongoClient client;
        public string connectionString = "mongodb+srv://jawad:jawad@cluster0.r6ob1.azure.mongodb.net/flare?retryWrites=true&w=majority";
        public string databaseName = "flare";
        public IMongoDatabase database;
        public string collectionName;
    }
}