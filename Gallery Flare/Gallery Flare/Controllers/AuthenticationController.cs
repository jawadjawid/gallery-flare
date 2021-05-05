using System;
using System.Threading.Tasks;
using Gallery_Flare.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gallery_Flare.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        Database database = new Database("user");

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost("SignUp")]
        public async Task<ActionResult> SignUpAsync([FromBody] UserModel user)
        {
            try
            {
                UserModel userResult = await database.GetUser(user.username);
                if (userResult != null)
                    return NotFound("User name is already taken!");

                string passwordHash = BCrypt.Net.BCrypt.HashPassword(user.password);
                await database.PostUser(user.username, passwordHash);
                return Ok();
            } catch (Exception)
            {
                return NotFound();
            }
       
        }

        [HttpPost("Login")]
        public async Task<ActionResult> LogInAsync([FromBody] UserModel user)
        {
            try
            {
                UserModel userResult = await database.GetUser(user.username);
                if (userResult == null)
                    throw new Exception();
                if(!BCrypt.Net.BCrypt.Verify(user.password, userResult.password))
                    throw new Exception();

                return Ok();
            }
            catch
            {
                return BadRequest("Invalid");
            }
        }
    }
}
