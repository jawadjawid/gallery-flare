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
using System.Net;

namespace GalleryFlareTests
{
    public class UploadTests : Tests
    {
        [Fact]
        public async Task UploadAnImageWitoutSignIn()
        {
            var webClient = new WebClient();
            byte[] payload = webClient.DownloadData("http://www.google.com/images/logos/ps_logo2.png");

            HttpClient httpClient = new HttpClient();
            MultipartFormDataContent multiContent = new MultipartFormDataContent();

            multiContent.Add(new ByteArrayContent(payload), "file", "google logo");
            var response = await httpClient.PostAsync($"{_baseUri}/Upload", multiContent).ConfigureAwait(false);

            Assert.Equal("BadRequest", response.StatusCode.ToString());
        }

        [Fact]
        public async Task UploadAnImageAfterSignIn()
        {
            var webClient = new WebClient();
            byte[] payload = webClient.DownloadData("http://www.google.com/images/logos/ps_logo2.png");

            HttpClient httpClient = new HttpClient();
            var httpContent = new StringContent("{\"username\":\"jawad\",\"password\":\"12345\"}", Encoding.UTF8, "application/json");
            await httpClient.PostAsync(_baseUri + "/Authentication/Login", httpContent);

            MultipartFormDataContent multiContent = new MultipartFormDataContent();

            multiContent.Add(new ByteArrayContent(payload), "file", "google logo");
            multiContent.Add(new StringContent("public"), "access");

            var response = await httpClient.PostAsync($"{_baseUri}/Upload", multiContent).ConfigureAwait(false);
            var contentString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            Assert.Equal("OK", response.StatusCode.ToString());
            Assert.Equal("uploaded", contentString);
        }

        [Fact]
        public async Task AbleToSeePublicImagesInPublicGallery()
        {
            var webClient = new WebClient();
            byte[] payload = webClient.DownloadData("http://www.google.com/images/logos/ps_logo2.png");

            HttpClient httpClient = new HttpClient();
            var httpContent = new StringContent("{\"username\":\"jawad\",\"password\":\"12345\"}", Encoding.UTF8, "application/json");
            await httpClient.PostAsync(_baseUri + "/Authentication/Login", httpContent);

            MultipartFormDataContent multiContent = new MultipartFormDataContent();

            multiContent.Add(new ByteArrayContent(payload), "file", "google logo");
            multiContent.Add(new StringContent("public"), "access");

            await httpClient.PostAsync($"{_baseUri}/Upload", multiContent).ConfigureAwait(false);

            HttpResponseMessage response = await httpClient.GetAsync(_baseUri + "/Gallery/public");
            var contentString = await response.Content.ReadAsStringAsync();

            Assert.Equal("OK", response.StatusCode.ToString());
            Assert.Contains("google", contentString);
        }

        [Fact]
        public async Task NotAbleToSeePrivateImagesInThePublicGallery()
        {
            var webClient = new WebClient();
            byte[] payload = webClient.DownloadData("https://galleryflare.blob.core.windows.net/flare/GettyImages-74411680-57f94bf75f9b586c35770f22.jpg");

            HttpClient httpClient = new HttpClient();
            var httpContent = new StringContent("{\"username\":\"jawad\",\"password\":\"12345\"}", Encoding.UTF8, "application/json");
            await httpClient.PostAsync(_baseUri + "/Authentication/Login", httpContent);

            MultipartFormDataContent multiContent = new MultipartFormDataContent();

            multiContent.Add(new ByteArrayContent(payload), "file", "olives");
            multiContent.Add(new StringContent("private"), "access");

            var response = await httpClient.PostAsync($"{_baseUri}/Upload", multiContent).ConfigureAwait(false);
            var contentString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            Assert.Equal("OK", response.StatusCode.ToString());
            Assert.DoesNotContain("olive", contentString);
        }
    }
}
