using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using ProjektZaliczeniowy2.Models;
using System.Linq;
using ProjektZaliczeniowy2;
using Microsoft.AspNetCore.Builder;

namespace TestProject1
{
    public class UserControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public UserControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAllUsers_ShouldReturnOk()
        {
            var response = await _client.GetAsync("/api/Users"); // poprawiony endpoint
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task CreateUser_ShouldReturnOk()
        {
            var newUser = new User
            {
                Name = "TestUser",
                Email = "testuser@example.com"
            };

            var response = await _client.PostAsJsonAsync("/api/Users", newUser); // poprawiony endpoint
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task DeleteUser_ShouldReturnNoContent()
        {
            var newUser = new User
            {
                Name = "ToDelete",
                Email = "delete@example.com"
            };

            var createResponse = await _client.PostAsJsonAsync("/api/Users", newUser); // poprawiony endpoint
            createResponse.EnsureSuccessStatusCode();

            var users = await _client.GetFromJsonAsync<User[]>("/api/Users"); // poprawiony endpoint

            Assert.NotNull(users);
            var userToDelete = users!.FirstOrDefault(u => u.Name == "ToDelete");

            Assert.NotNull(userToDelete);

            var deleteResponse = await _client.DeleteAsync($"/api/Users/{userToDelete!.Id}"); // poprawiony endpoint
            Assert.Equal(System.Net.HttpStatusCode.NoContent, deleteResponse.StatusCode);
        }
    }
}
