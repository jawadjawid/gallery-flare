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
        public string connectionString = "";
        public string databaseName = "flare";
        public IMongoDatabase database;
        public string collectionName;
    }
}