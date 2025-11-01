using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Voluntariado.Data;
using Voluntariado.Models;

namespace Voluntariado.Pages.Roles
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
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

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var redirect = VerificarAcceso();
            if (redirect != null)
                return redirect;

            Role = await _context.Roles.FindAsync(id);
            if (Role == null)
                return NotFound();

            if (Role.Name.Trim().ToLower() == "administrador")
            {
                TempData["ErrorMessage"] = "⚠️ No se puede eliminar el rol de Administrador.";
                return RedirectToPage("/Roles/Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var redirect = VerificarAcceso();
            if (redirect != null)
                return redirect;

            var role = await _context.Roles.FindAsync(Role.Id);
            if (role == null)
                return NotFound();

            if (role.Name.Trim().ToLower() == "administrador")
            {
                TempData["ErrorMessage"] = "⚠️ No se puede eliminar el rol de Administrador.";
                return RedirectToPage("/Roles/Index");
            }

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"✅ Rol '{role.Name}' eliminado correctamente.";
            return RedirectToPage("/Roles/Index");
        }
    }
}
