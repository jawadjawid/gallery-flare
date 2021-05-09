using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gallery_Flare.Controllers.Operations;
using Gallery_Flare.Controllers.Operations.Database;
using Gallery_Flare.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Gallery_Flare.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly AzureStoarge azureStoarge;

        private readonly GalleryDatabaseConnector database;

        public SearchController(GalleryDatabaseConnector database, AzureStoarge azureStoarge)
        {
            this.database = database;
            this.azureStoarge = azureStoarge;
        }

        [HttpGet("{query}")]
        public async Task<string> SearchAsync(string query)
        {
            try
            {
                return JsonConvert.SerializeObject(await database.GetImagesFromDbAsync(access: "public", tags: query));
            }
            catch (Exception)
            {
                return JsonConvert.SerializeObject(new List<ImageModel>());
            }
        }

        [HttpPost]
        public async Task<string> Search([FromForm] IFormFile file)
        {
            try
            {
                string blobUrl = await azureStoarge.UploadAsync(file.OpenReadStream(), file.FileName);
                string tags = await new TagImages(blobUrl).MostCommonTags(20);

                return JsonConvert.SerializeObject(await database.GetImagesFromDbAsync(access: "public", tags: tags));
            }
            catch (Exception)
            {
                return JsonConvert.SerializeObject(new List<ImageModel>());
            }
        }
    }
}
