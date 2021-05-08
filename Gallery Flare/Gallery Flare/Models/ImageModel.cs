using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gallery_Flare.Models
{
    [BsonIgnoreExtraElements]
    public class ImageModel
    {
        public string img;
        public string title;
        public string author;
        public string tags;
        public string access;
    }
}
