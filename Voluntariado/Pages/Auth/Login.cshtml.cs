using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Voluntariado.Data;
using Voluntariado.Models;
using Voluntariado.Services;
using System.Linq;
using BCrypt.Net;

namespace Voluntariado.Pages.Auth
{
    public class LoginModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher _passwordHasher;

        public LoginModel(ApplicationDbContext context, IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        [BindProperty]
        public string Email { get; set; } = string.Empty;

        [BindProperty]
        public string Password { get; set; } = string.Empty;

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                TempData["ErrorMessage"] = "Debe ingresar un correo y una contraseña.";
                return Page();
            }

            // Buscar por correo electrónico en lugar de Username
            var user = _context.Users.FirstOrDefault(u => u.Email == Email);

            if (user == null)
            {
                TempData["ErrorMessage"] = "No existe una cuenta con ese correo.";
                return Page();
            }

            // Verificar contraseña
            if (!BCrypt.Net.BCrypt.Verify(Password, user.PasswordHash))
            {
                TempData["ErrorMessage"] = "Contraseña incorrecta.";
                return Page();
            }

            // ✅ Guardar datos básicos en sesión
            HttpContext.Session.SetString("UserId", user.Id.ToString());
            HttpContext.Session.SetString("Email", user.Email);
            HttpContext.Session.SetString("Role", user.Role?.Name ?? "");

            // ✅ Redirigir según rol
            if (user.Role?.Name == "Ofertante")
                return RedirectToPage("/VolunteerOffers/Index");

            if (user.Role?.Name == "Administrador")
                return RedirectToPage("/Admin/Dashboard");

            // Por defecto, voluntario
            return RedirectToPage("/Index");
        }
    }
}
