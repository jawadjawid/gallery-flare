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
    public class GalleryTests : Tests
    {
        [Fact]
        public async Task GalleryGetPublic()
        {
            var client = new HttpClient();
            using var postContent = new MultipartFormDataContent("boundary_" + DateTime.Now.ToString(CultureInfo.InvariantCulture));

            HttpResponseMessage response = await client.GetAsync(_baseUri + "/Gallery/public");
            var contentString = await response.Content.ReadAsStringAsync();

            Assert.Equal("OK", response.StatusCode.ToString());
            Assert.Contains("\"access\":\"public\"", contentString);
            Assert.DoesNotContain("\"access\":\"private\"", contentString);
        }

        [Fact]
        public async Task GalleryGetPrivateWithNoLogin()
        {
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(_baseUri + "/Gallery/personal");
            var contentString = await response.Content.ReadAsStringAsync();

            Assert.Equal("OK", response.StatusCode.ToString());
            Assert.Equal("[]", contentString);
        }

        [Fact]
        public async Task GalleryGetPrivateAfterLogin()
        {
            var client = new HttpClient();

            var httpContent = new StringContent("{\"username\":\"jawad\",\"password\":\"12345\"}", Encoding.UTF8, "application/json");
            await client.PostAsync(_baseUri + "/Authentication/Login", httpContent);

            HttpResponseMessage galleryResponse = await client.GetAsync(_baseUri + "/Gallery/personal");
            var contentString = await galleryResponse.Content.ReadAsStringAsync();

            Assert.Equal("OK", galleryResponse.StatusCode.ToString());

            Assert.Contains("\"author\":\"jawad\"", contentString);
            Assert.Contains("galleryflare.blob.core.windows.net", contentString);
        }
    }
}
