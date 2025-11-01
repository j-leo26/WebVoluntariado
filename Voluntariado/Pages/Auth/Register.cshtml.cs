using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Voluntariado.Data;
using Voluntariado.Models;
using BCrypt.Net;
using System.Linq;

namespace Voluntariado.Pages.Auth
{
    public class RegisterModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public RegisterModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string FirstName { get; set; } = string.Empty;

        [BindProperty]
        public string LastName { get; set; } = string.Empty;

        [BindProperty]
        public string Username { get; set; } = string.Empty;

        [BindProperty]
        public string Email { get; set; } = string.Empty;

        [BindProperty]
        public string Password { get; set; } = string.Empty;

        [BindProperty]
        public int RoleId { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                TempData["ErrorMessage"] = "Debe llenar todos los campos obligatorios.";
                return Page();
            }

            // Verificar si ya existe el usuario
            if (_context.Users.Any(u => u.Email == Email))
            {
                TempData["ErrorMessage"] = "Ya existe una cuenta con ese correo.";
                return Page();
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(Password);

            var user = new User
            {
                FirstName = FirstName,
                LastName = LastName,
                Username = Username,
                Email = Email,
                PasswordHash = hashedPassword,
                RoleId = RoleId,
                CreatedByAdmin = false
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Usuario registrado exitosamente. Ahora puede iniciar sesión.";
            return RedirectToPage("/Auth/Login");
        }
    }
}
