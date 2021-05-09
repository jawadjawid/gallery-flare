using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using Gallery_Flare.Models;
using Newtonsoft.Json;
using Gallery_Flare.Controllers.Operations;
using Gallery_Flare.Controllers.Operations.Database;

namespace Gallery_Flare.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GalleryController : ControllerBase
    {
        private readonly UserService userService;

        private readonly GalleryDatabaseConnector database;

        public GalleryController(GalleryDatabaseConnector database, UserService userService)
        {
            this.database = database;
            this.userService = userService;
        }

        [HttpGet("{access}")]
        public async Task<string> GetAsync(string access)
        {
            IList<ImageModel> results = new List<ImageModel>();
            try
            {
                if (!(access == "public" || access == "personal"))
                    throw new Exception();

                if (access == "personal")
                {
                    UserModel user = await userService.GetCurrentUserAsync(Request.Cookies["jwt"]);
                    results = await database.GetImagesFromDbAsync(author: user.username);
                }
                else
                {
                    results = await database.GetImagesFromDbAsync(access: "public");

                }
                return JsonConvert.SerializeObject(results);
            }
            catch (Exception)
            {
                return JsonConvert.SerializeObject(results);
            }
        }     
    }
}
