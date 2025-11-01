using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Voluntariado.Data;
using Voluntariado.Services;
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

        [BindProperty(SupportsGet = true)]
        public string Mensaje { get; set; } = string.Empty;

        public void OnGet() {
            // Recuperar mensaje desde Session (si existe) y luego removerlo
            Mensaje = HttpContext.Session.GetString("WarningMessage");
            if (!string.IsNullOrEmpty(Mensaje))
            {
                HttpContext.Session.Remove("WarningMessage");
            }
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                TempData["ErrorMessage"] = "Debe ingresar un correo y una contraseña.";
                return Page();
            }

            // Cargar el usuario incluyendo su Rol
            var user = _context.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Email == Email);

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

            // Guardar datos en sesión
            HttpContext.Session.SetString("UserId", user.Id.ToString());
            HttpContext.Session.SetString("Email", user.Email);

            var roleName = user.Role?.Name ?? string.Empty;
            HttpContext.Session.SetString("Role", roleName);

            // Redirigir según rol
            switch (roleName)
            {
                case "Administrador":
                    return RedirectToPage("/Admin/Index");
                case "Ofertante":
                    return RedirectToPage("/VolunteerOffers/Index");
                case "Voluntario":
                    return RedirectToPage("/Index");
                default:
                    return RedirectToPage("/Index");
            }
        }
    }
}
