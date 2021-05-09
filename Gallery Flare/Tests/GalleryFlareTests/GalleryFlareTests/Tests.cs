using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Globalization;
using System.Text;

namespace GalleryFlareTests
{
    public class Tests
    {
        public string _baseUri = "https://localhost:44339";

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
