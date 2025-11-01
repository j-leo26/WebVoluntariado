using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Voluntariado.Data;
using Voluntariado.Models;

namespace Voluntariado.Pages.Roles
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Role> Roles { get; set; } = new List<Role>();

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

        public async Task<IActionResult> OnGetAsync()
        {
            var redirect = VerificarAcceso();
            if (redirect != null)
                return redirect;

            Roles = await _context.Roles.ToListAsync();
            return Page();
        }
    }
}
