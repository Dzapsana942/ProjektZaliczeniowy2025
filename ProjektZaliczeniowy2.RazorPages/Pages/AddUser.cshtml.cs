using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ProjektZaliczeniowy2.RazorPages.Pages
{
    public class AddUserModel : PageModel
    {
        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string Email { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoidGVzdHVzZXIiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJ0ZXN0QHRlc3QuY29tIiwiZXhwIjoxNzQ5NzU3MTQyfQ.uAuQO8ZdbStm_xhAeGmht_iz0v9JeWFINPdPpw5Ap9c";

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var user = new
            {
                Name = Name,
                Email = Email
            };

            var content = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:7233/api/User", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("UserList");
            }

            ModelState.AddModelError(string.Empty, "Error adding user");
            return Page();
        }
    }
}
