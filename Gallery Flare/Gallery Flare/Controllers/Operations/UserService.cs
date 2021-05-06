using Gallery_Flare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gallery_Flare.Controllers.Operations
{
    public class UserService
    {
        public async Task<UserModel> GetCurrentUserAsync(string jwt)
        {
            try
            {
                JWTService jWTService = new JWTService();
                Database database = new Database("user");

                var token = jWTService.Verify(jwt);
                UserModel user = await database.GetUser(token.Issuer);
                if (user == null)
                    throw new Exception();
                return user;
            }
            catch
            {
                throw new Exception();
            }
     
        }

    }
}
