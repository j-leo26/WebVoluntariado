using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Voluntariado.Data;
using Voluntariado.Models;
using Voluntariado.Services;
using System.Linq;

namespace Voluntariado.Pages
{
    public class TestSessionModel : PageModel
    {
        public string? Username { get; set; }
        public string? Role { get; set; }

        public void OnGet()
        {
            Username = HttpContext.Session.GetString("Username");
            Role = HttpContext.Session.GetString("Role");
        }
    }
}
