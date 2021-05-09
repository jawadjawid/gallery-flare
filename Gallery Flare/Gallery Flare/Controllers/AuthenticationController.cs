using System;
using System.Threading.Tasks;
using Gallery_Flare.Controllers.Operations;
using Gallery_Flare.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gallery_Flare.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        Database database = new Database("user");

        [HttpPost("SignUp")]
        public async Task<ActionResult> SignUpAsync([FromBody] UserModel user)
        {
            try
            {
                UserModel userResult = await database.GetUser(user.username);
                if (userResult != null)
                    return BadRequest("User name is already taken!");

                string passwordHash = BCrypt.Net.BCrypt.HashPassword(user.password);
                await database.PostUser(user.username, passwordHash);
                return Ok();
            } catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpPost("Login")]
        public async Task<ActionResult> LogInAsync([FromBody] UserModel user)
        {
            try
            {
                UserModel userResult = await database.GetUser(user.username);
                if (userResult == null || !BCrypt.Net.BCrypt.Verify(user.password, userResult.password))
                    throw new Exception();
                JWTService jWTService = new JWTService();
                Response.Cookies.Append("jwt", jWTService.Generate(userResult.username), new Microsoft.AspNetCore.Http.CookieOptions { HttpOnly = true });

                return Ok(userResult.username);
            }
            catch
            {
                return BadRequest("Invalid");
            }
        }

        [HttpGet("Logout")]
        public IActionResult Logout()
        {
            try
            {
                Response.Cookies.Delete("jwt");
                return Ok();
            }
            catch
            {
                return BadRequest("Invalid");

            }
        }

        [HttpGet("User")]
        public async Task<IActionResult> GetUserNameAsync()
        {
            try
            {
                UserService userService = new UserService();
                UserModel user = await userService.GetCurrentUserAsync(Request.Cookies["jwt"]);
                return Ok(user.username);
            }
            catch
            {
                return Unauthorized();
            }
        }
    }
}
