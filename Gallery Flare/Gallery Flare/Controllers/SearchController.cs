using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gallery_Flare.Controllers.Operations;
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
        [HttpGet("{query}")]
        public async Task<string> SearchAsync(string query)
        {
            IList<ImageModel> results = new List<ImageModel>();
            try
            {
                Database database = new Database("images");
                results = await database.GetImagesFromDbAsync(access: "public", tags: query);
                return JsonConvert.SerializeObject(results);
            }
            catch (Exception)
            {
                return JsonConvert.SerializeObject(results);
            }
        }

        [HttpPost]
        public async Task<string> Search([FromForm] IFormFile file, [FromForm] string access = "Search")
        {
            IList<ImageModel> results = new List<ImageModel>();
            try
            {
                AzureStoarge azureStoarge = new AzureStoarge();
                string blobUrl = await azureStoarge.UploadAsync(file.OpenReadStream(), file.FileName);

                TagImages search = new TagImages(blobUrl);
                string tags = await search.MostCommonTags(20);

                Database database = new Database("images");
                //await database.PostImageToDbAsync(file.FileName, blobUrl, "jawadjawid", access, tags);

                results = await database.GetImagesFromDbAsync(access: "public", tags: tags);

                return JsonConvert.SerializeObject(results);
            }
            catch (Exception)
            {
                return JsonConvert.SerializeObject(results);
            }
        }


    }
}
