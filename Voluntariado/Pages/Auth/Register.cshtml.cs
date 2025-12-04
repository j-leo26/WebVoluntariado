using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Voluntariado.Data;
using Voluntariado.Models;
using BCrypt.Net;
using System.Threading.Tasks;

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

        public async Task<IActionResult> OnPostAsync()
        {
            // ✅ Validar campos obligatorios
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password) ||
                string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName) ||
                string.IsNullOrWhiteSpace(Username) || RoleId == 0)
            {
                TempData["ErrorMessage"] = "Debe llenar todos los campos obligatorios.";
                return Page();
            }

            // ✅ Validar duplicado de email
            if (await _context.Users.AnyAsync(u => u.Email == Email))
            {
                TempData["ErrorMessage"] = "Ya existe una cuenta con ese correo.";
                return Page();
            }

            // ✅ Verificar que el rol exista en la base de datos
            var role = await _context.Roles.FindAsync(RoleId);
            if (role == null)
            {
                // Si el rol no existe, asignamos Voluntario por defecto
                role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "Voluntario");
                if (role == null)
                {
                    TempData["ErrorMessage"] = "No se encontró el rol por defecto. Contacte al administrador.";
                    return Page();
                }
                RoleId = role.Id;
            }

            // ✅ Encriptar contraseña
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(Password);

            // ✅ Crear el nuevo usuario
            var user = new User
            {
                FirstName = FirstName,
                LastName = LastName,
                Username = Username,
                Email = Email,
                PasswordHash = hashedPassword,
                RoleId = RoleId,
                CreatedByAdmin = false,
                CreatedAt = DateTime.Now
            };

            // ✅ Guardar en BD
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Registro exitoso. Ahora puede iniciar sesión.";
            return RedirectToPage("/Auth/Login");
        }
    }
}
