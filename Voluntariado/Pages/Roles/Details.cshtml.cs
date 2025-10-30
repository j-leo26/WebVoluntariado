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

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Role = await _context.Roles.FindAsync(id);
            if (Role == null)
                return NotFound();

            return Page();
        }
    }
}
