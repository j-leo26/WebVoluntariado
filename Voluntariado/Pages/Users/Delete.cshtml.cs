using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Voluntariado.Data;
using Voluntariado.Models;

namespace Voluntariado.Pages.Users
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public User User { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            User = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (User == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var userToDelete = await _context.Users.FindAsync(id);

            if (userToDelete == null)
                return NotFound();

            _context.Users.Remove(userToDelete);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "🗑️ Usuario eliminado correctamente.";
            return RedirectToPage("Index");
        }
    }
}
