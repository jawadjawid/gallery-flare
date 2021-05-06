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

namespace Gallery_Flare.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GalleryController : ControllerBase
    {
        [HttpGet("{access}")]
        public async Task<string> GetAsync(string access)
        {
            IList<ImageModel> results = new List<ImageModel>();
            try
            {
                if (!(access == "public" || access == "personal"))
                    throw new Exception();

                Database database = new Database("images");

                if (access == "personal")
                {
                    UserService userService = new UserService();
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
