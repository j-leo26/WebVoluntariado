using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Voluntariado.Data;
using Voluntariado.Models;
using System.Threading.Tasks;
using System.Linq;
using BCrypt.Net; // 👈 importante para usar BCrypt directamente

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
        public User User { get; set; } = new User();

        [BindProperty]
        public string Password { get; set; } = string.Empty;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            // Validar usuario existente
            if (_context.Users.Any(u => u.Email == User.Email))
            {
                TempData["ErrorMessage"] = "Ya existe una cuenta con ese correo.";
                return Page();
            }

            // ✅ Encriptar contraseña con BCrypt antes de guardar
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(Password);
            User.PasswordHash = hashedPassword;

            // Asignar rol por defecto (por ejemplo, voluntario)
            var defaultRole = _context.Roles.FirstOrDefault(r => r.Name == "Voluntario");
            if (defaultRole != null)
                User.RoleId = defaultRole.Id;

            _context.Users.Add(User);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Registro exitoso. Ahora puedes iniciar sesión.";
            return RedirectToPage("/Auth/Login");
        }
    }
}
