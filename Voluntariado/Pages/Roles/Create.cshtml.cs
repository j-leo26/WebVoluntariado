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

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            _context.Roles.Add(Role);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
