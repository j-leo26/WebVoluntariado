using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Voluntariado.Data;
using Voluntariado.Models;

namespace Voluntariado.Pages.Roles
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Role Role { get; set; } = new Role();

        private IActionResult? VerificarAcceso()
        {
            var role = HttpContext.Session.GetString("Role");

            if (string.IsNullOrEmpty(role))
                return RedirectToPage("/Auth/Login");

            if (role != "Administrador")
            {
                TempData["ErrorMessage"] = "⚠️ No tienes permiso para acceder a esta página, inicia sesión con un rol autorizado.";
                return RedirectToPage("/Auth/Login");
            }

            return null;
        }

        public IActionResult OnGet()
        {
            var redirect = VerificarAcceso();
            if (redirect != null)
                return redirect;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var redirect = VerificarAcceso();
            if (redirect != null)
                return redirect;

            if (!ModelState.IsValid)
                return Page();

            _context.Roles.Add(Role);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"✅ Rol '{Role.Name}' creado correctamente.";
            return RedirectToPage("Index");
        }
    }
}
