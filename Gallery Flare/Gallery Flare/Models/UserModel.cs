using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gallery_Flare.Models
{
    [BsonIgnoreExtraElements]
    public class UserModel
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}
