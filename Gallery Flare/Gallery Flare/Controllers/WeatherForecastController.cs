using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using Gallery_Flare.Models;
using Newtonsoft.Json;


namespace Gallery_Flare.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        //private static readonly string[] Summaries = new[]
        //{
        //    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        //};

        //private readonly ILogger<WeatherForecastController> _logger;

        //public WeatherForecastController(ILogger<WeatherForecastController> logger)
        //{
        //    _logger = logger;
        //}

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

        public async Task<int> PostToDbAsync(string fileName, string fileURL, string userID, string access)
        {
            var client = new MongoClient("mongodb+srv://jawad:jawad@cluster0.r6ob1.azure.mongodb.net/flare?retryWrites=true&w=majority");

            var database = client.GetDatabase("flare");
            var collection = database.GetCollection<BsonDocument>("images");

            var command = new BsonDocument { { "title", fileName }, { "img", fileURL }, { "author", userID }, { "access", access } };
            await collection.InsertOneAsync(command);
            var id = command;
            return 1;
        }

        [HttpPost]
        public async Task<OkObjectResult> PostAsync([FromForm] IFormFile file, [FromForm] string access)
        {
            try
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=galleryflare;AccountKey=qEzmObr5GKhv8XTXXl0PEOAQ3J/hVZM+tbpllTbRv1uHK76OCeeD0AufUsyoguYeaidqTWKEkl2nbB1LMK6wLw==;EndpointSuffix=core.windows.net");
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference("flare");
                var blob = container.GetBlockBlobReference($"{file.FileName}_{DateTimeOffset.Now.ToUnixTimeMilliseconds()}");

                await blob.UploadFromStreamAsync(file.OpenReadStream());
                var blobUrl = blob.Uri.AbsoluteUri;

                await PostToDbAsync(file.FileName, blobUrl, "jawadjawid", access);

                return Ok("uploaded");

            } catch(Exception e)
            {
                return Ok("fail");
            }
        }
    }
}
