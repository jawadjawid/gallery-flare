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
        private string _baseUri = "https://localhost:44339";

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

        [Fact]
        public async Task LoginWithInvalidCredentials()
        {
            var client = new HttpClient();

            var httpContent = new StringContent("{\"username\":\"jawad\",\"password\":\"123456\"}", Encoding.UTF8, "application/json");
            var response = await client.PostAsync(_baseUri + "/Authentication/Login", httpContent);

            var contentString = await response.Content.ReadAsStringAsync();

            Assert.Equal("BadRequest", response.StatusCode.ToString());
            Assert.Equal("Invalid", contentString);
        }

        [Fact]
        public async Task LoginWithValidCredentials()
        {
            var client = new HttpClient();

            var httpContent = new StringContent("{\"username\":\"jawad\",\"password\":\"12345\"}", Encoding.UTF8, "application/json");
            var response = await client.PostAsync(_baseUri + "/Authentication/Login", httpContent);

            var contentString = await response.Content.ReadAsStringAsync();

            Assert.Equal("OK", response.StatusCode.ToString());
            Assert.Equal("jawad", contentString);
        }

        [Fact]
        public async Task SignupWithUsedUserName()
        {
            var client = new HttpClient();

            var httpContent = new StringContent("{\"username\":\"jawad\",\"password\":\"12345\"}", Encoding.UTF8, "application/json");
            var response = await client.PostAsync(_baseUri + "/Authentication/Signup", httpContent);

            var contentString = await response.Content.ReadAsStringAsync();

            Assert.Equal("BadRequest", response.StatusCode.ToString());
            Assert.Equal("User name is already taken!", contentString);
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        [Fact]
        public async Task SignupWithNewUserName()
        {
            var client = new HttpClient();
            string randomUserName = RandomString(8);
            var httpContent = new StringContent($"{{\"username\":\"{randomUserName}\",\"password\":\"12345\"}}", Encoding.UTF8, "application/json");
            var response = await client.PostAsync(_baseUri + "/Authentication/Signup", httpContent);

            var contentString = await response.Content.ReadAsStringAsync();

            Assert.Equal("OK", response.StatusCode.ToString());
        }

        [Fact]
        public async Task GetLoggedInUserInfoAfterLogin()
        {
            var client = new HttpClient();

            var httpContent = new StringContent("{\"username\":\"jawad\",\"password\":\"12345\"}", Encoding.UTF8, "application/json");
            await client.PostAsync(_baseUri + "/Authentication/Login", httpContent);

            HttpResponseMessage userDataResponse = await client.GetAsync(_baseUri + "/Authentication/User");
            var contentString = await userDataResponse.Content.ReadAsStringAsync();

            Assert.Equal("OK", userDataResponse.StatusCode.ToString());
            Assert.Contains("jawad", contentString);
        }

        [Fact]
        public async Task GetLoggedInUserInfoWithoutLogin()
        {
            var client = new HttpClient();

            HttpResponseMessage userDataResponse = await client.GetAsync(_baseUri + "/Authentication/User");
            await userDataResponse.Content.ReadAsStringAsync();

            Assert.Equal("Unauthorized", userDataResponse.StatusCode.ToString());
        }

        [Fact]
        public async Task GetLoggedInUserAfterLogout()
        {
            var client = new HttpClient();

            var httpContent = new StringContent("{\"username\":\"jawad\",\"password\":\"12345\"}", Encoding.UTF8, "application/json");
            await client.PostAsync(_baseUri + "/Authentication/Login", httpContent);
            await client.GetAsync(_baseUri + "/Authentication/Logout");

            HttpResponseMessage userDataResponse = await client.GetAsync(_baseUri + "/Authentication/User");
            await userDataResponse.Content.ReadAsStringAsync();

            Assert.Equal("Unauthorized", userDataResponse.StatusCode.ToString());
        }
    }
}
