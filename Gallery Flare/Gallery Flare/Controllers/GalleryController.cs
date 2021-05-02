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
                Database database = new Database("images");
                results = await database.GetFromDbAsync();
                return JsonConvert.SerializeObject(results);
            }
            catch (Exception)
            {
                return JsonConvert.SerializeObject(results);
            }
        }
    }
}
