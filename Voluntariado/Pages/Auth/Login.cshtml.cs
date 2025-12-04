using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Voluntariado.Data;
using Voluntariado.Services;
using BCrypt.Net;
using System.Threading.Tasks;
using System;

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

        [BindProperty(SupportsGet = true)]
        public string Mensaje { get; set; } = string.Empty;

        // Mostrar mensajes de debug o Warning guardados en Session (si existen)
        public void OnGet()
        {
            var warning = HttpContext.Session.GetString("WarningMessage");
            if (!string.IsNullOrEmpty(warning))
            {
                Mensaje = warning;
                HttpContext.Session.Remove("WarningMessage");
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Validaciones básicas
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                TempData["ErrorMessage"] = "Debe ingresar un correo y una contraseña.";
                return Page();
            }

            // Cargar el usuario incluyendo su Role
            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == Email);

            if (user == null)
            {
                TempData["ErrorMessage"] = "No existe una cuenta con ese correo.";
                return Page();
            }

            // Verificar contraseña (BCrypt)
            if (!BCrypt.Net.BCrypt.Verify(Password, user.PasswordHash))
            {
                TempData["ErrorMessage"] = "Contraseña incorrecta.";
                return Page();
            }

            // Resolver nombre del rol de forma robusta:
            string roleName = user.Role?.Name;

            if (string.IsNullOrEmpty(roleName))
            {
                // Si Role es null (por alguna razón), buscar por RoleId
                roleName = await _context.Roles
                    .Where(r => r.Id == user.RoleId)
                    .Select(r => r.Name)
                    .FirstOrDefaultAsync();
            }

            roleName = roleName?.Trim() ?? string.Empty;

            // Guardar datos en sesión
            HttpContext.Session.SetString("UserId", user.Id.ToString());
            HttpContext.Session.SetString("Email", user.Email);
            HttpContext.Session.SetString("Role", roleName);

            // Depuración temporaria: guardamos un Debug en TempData (muestra en la vista)
            TempData["DebugRole"] = $"UserId={user.Id}, RoleId={user.RoleId}, ResolvedRole='{roleName}'";

            // Redirección robusta (comparación case-insensitive)
            if (roleName.Equals("Administrador", StringComparison.OrdinalIgnoreCase))
                return RedirectToPage("/Admin/Index");

            if (roleName.Equals("Ofertante", StringComparison.OrdinalIgnoreCase))
                return RedirectToPage("/VolunteerOffers/Index");

            if (roleName.Equals("Voluntario", StringComparison.OrdinalIgnoreCase))
                return RedirectToPage("/Index");

            // Si no sabemos el rol, mostramos mensaje útil y el debug
            TempData["ErrorMessage"] = "No se pudo identificar correctamente el rol del usuario. Contacta al administrador.";
            return Page();
        }
    }
}
