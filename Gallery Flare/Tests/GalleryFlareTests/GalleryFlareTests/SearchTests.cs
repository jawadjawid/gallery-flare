using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using System.Globalization;
using System.Net;
using System.IO;
using System.Drawing;
using System.Text;

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

        [Fact]
        public async Task SearchByMatchingImage()
        {
            var webClient = new WebClient();
            byte[] payload = webClient.DownloadData("http://www.google.com/images/logos/ps_logo2.png");

            HttpClient httpClient = new HttpClient();
            MultipartFormDataContent multiContent = new MultipartFormDataContent();

            multiContent.Add(new ByteArrayContent(payload), "file", "google logo");
            var response = await httpClient.PostAsync($"{_baseUri}/Search", multiContent).ConfigureAwait(false);
            var contentString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            Assert.Equal("OK", response.StatusCode.ToString());
            Assert.Contains("\"tags\":\"logo", contentString);
        }

        [Fact]
        public async Task SearchByANonMatchingImage()
        {
            var webClient = new WebClient();
            byte[] payload = webClient.DownloadData("https://galleryflare.blob.core.windows.net/flare/iphone-12-pro-graphite-hero.png");

            HttpClient httpClient = new HttpClient();
            MultipartFormDataContent multiContent = new MultipartFormDataContent();

            multiContent.Add(new ByteArrayContent(payload), "file", "iphone");
            var response = await httpClient.PostAsync($"{_baseUri}/Search", multiContent).ConfigureAwait(false);
            var contentString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            Assert.Equal("OK", response.StatusCode.ToString());
            Assert.Equal("[]", contentString);
        }
    }
}
