using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Voluntariado.Data;
using Voluntariado.Models;

namespace Voluntariado.Pages.Roles
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

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

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var redirect = VerificarAcceso();
            if (redirect != null)
                return redirect;

            Role = await _context.Roles.FindAsync(id);
            if (Role == null)
                return NotFound();

            return Page();
        }
    }
}
