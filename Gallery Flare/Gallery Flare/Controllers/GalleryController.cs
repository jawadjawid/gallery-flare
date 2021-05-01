using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using Gallery_Flare.Models;
using Newtonsoft.Json;


namespace Gallery_Flare.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GalleryController : ControllerBase
    {
        [HttpGet]
        public async Task<string> GetAsync()
        {
            IList<ImageModel> results = new List<ImageModel>();
            try
            {
                var client = new MongoClient("mongodb+srv://jawad:jawad@cluster0.r6ob1.azure.mongodb.net/flare?retryWrites=true&w=majority");
                var database = client.GetDatabase("flare");
                var collection = database.GetCollection<BsonDocument>("images");
                var documents = await collection.Find(new BsonDocument()).ToListAsync();
                foreach (var doc in documents)
                    results.Add(BsonSerializer.Deserialize<ImageModel>(doc));
                return JsonConvert.SerializeObject(results);
            }
            catch (Exception e)
            {
                return JsonConvert.SerializeObject(results);
            }
        }
    }
}
