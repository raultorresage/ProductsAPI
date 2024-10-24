using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using ProductsAPI.Models;
using System.Net.Http.Json;

namespace Tests
{
    public class UserTests: IClassFixture<WebApplicationFactory<Program>>
    {

        private readonly HttpClient _client;

        public UserTests()
        {
            WebApplicationFactory<Program> factory = new WebApplicationFactory<Program>();
            this._client = factory.CreateClient();
        }

        [Theory]
        [InlineData("user", "password")]
        [InlineData("raul", "123")]
        [InlineData("admin", "admin")]
        public void UserMustLogIn(string u, string p)
        {
            u.Should().NotBeNullOrEmpty();
            p.Should().NotBeNullOrEmpty();

            User user = new User(u, p);
            this._client.PostAsJsonAsync("api/user/login", user).Result.Should().HaveStatusCode(System.Net.HttpStatusCode.OK);
        }
    }
}