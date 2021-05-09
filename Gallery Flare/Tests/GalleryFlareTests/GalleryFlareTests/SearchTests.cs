using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using System.Globalization;
using System.Net;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace GalleryFlareTests
{
    public class SearchTests : Tests
    {
        [Fact]
        public async Task SearchByTagWithMatches()
        {
            var client = new HttpClient();
            using var postContent = new MultipartFormDataContent("boundary_" + DateTime.Now.ToString(CultureInfo.InvariantCulture));

            HttpResponseMessage response = await client.GetAsync(_baseUri + "/Search/messi");
            var contentString = await response.Content.ReadAsStringAsync();

            Assert.Equal("OK", response.StatusCode.ToString());
            Assert.Contains("\"tags\":\"messi", contentString);
            Assert.Contains("galleryflare.blob.core.windows.net", contentString);
        }

        [Fact]
        public async Task SearchByTagWithNoMatches()
        {
            var client = new HttpClient();
            using var postContent = new MultipartFormDataContent("boundary_" + DateTime.Now.ToString(CultureInfo.InvariantCulture));

            HttpResponseMessage response = await client.GetAsync(_baseUri + $"/Search/{RandomString(8)}");
            var contentString = await response.Content.ReadAsStringAsync();

            Assert.Equal("OK", response.StatusCode.ToString());
            Assert.Equal("[]", contentString);
        }
    }
}
