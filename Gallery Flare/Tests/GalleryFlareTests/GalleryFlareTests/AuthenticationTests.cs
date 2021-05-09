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
    public class AuthenticationTests : Tests
    {
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
