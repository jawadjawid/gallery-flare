using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Gallery_Flare.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UploadController : Controller
    {
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromForm] IFormFile file, [FromForm] string access)
        {
            try
            {
                AzureStoarge azureStoarge = new AzureStoarge();
                string blobUrl = await azureStoarge.UploadAsync(file.OpenReadStream(), file.FileName);
         
                TagImages search = new TagImages(blobUrl);
                string tags = await search.MostCommonTags(20);

                Database database = new Database("images");
                await database.PostImageToDbAsync(file.FileName, blobUrl, "jawadjawid", access, tags);

                return Ok("uploaded");
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

    }
}
