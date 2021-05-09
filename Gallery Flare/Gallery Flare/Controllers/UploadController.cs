using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gallery_Flare.Controllers.Operations;
using Gallery_Flare.Controllers.Operations.Database;
using Gallery_Flare.Models;
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
        private readonly UserService userService;

        private readonly AzureStoarge azureStoarge;

        private readonly GalleryDatabaseConnector database;

        public UploadController(UserService userService, AzureStoarge azureStoarge, GalleryDatabaseConnector database)
        {
            this.userService = userService;
            this.azureStoarge = azureStoarge;
            this.database = database;
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync([FromForm] IFormFile file, [FromForm] string access)
        {
            try
            {
                UserModel user = await userService.GetCurrentUserAsync(Request.Cookies["jwt"]);
                string blobUrl = await azureStoarge.UploadAsync(file.OpenReadStream(), file.FileName);
         
                string tags = await new TagImages(blobUrl).MostCommonTags(20);

                await database.PostImageToDbAsync(file.FileName, blobUrl, user.username, access, tags);

                return Ok("uploaded");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

    }
}
