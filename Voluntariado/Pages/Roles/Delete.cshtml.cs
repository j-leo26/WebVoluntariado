using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Voluntariado.Data;
using Voluntariado.Models;
using System.Threading.Tasks;

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

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Role = await _context.Roles.FindAsync(id);
            if (Role == null)
                return NotFound();

            // 🚫 Bloquear acceso a eliminar “Administrador”
            if (Role.Name.Trim().ToLower() == "administrador")
            {
                TempData["ErrorMessage"] = "⚠️ No se puede eliminar el rol de Administrador.";
                return RedirectToPage("/Roles/Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var role = await _context.Roles.FindAsync(Role.Id);
            if (role == null)
                return NotFound();

            // 🚫 Evitar eliminación del rol “Administrador”
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
