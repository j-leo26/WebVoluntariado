using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Voluntariado.Data;
using Voluntariado.Models;

namespace Voluntariado.Pages.Roles
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public EditModel(ApplicationDbContext context)
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

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            _context.Attach(Role).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
