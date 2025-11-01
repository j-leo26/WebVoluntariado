using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Voluntariado.Data;
using Voluntariado.Models;

namespace Voluntariado.Pages.Users
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public DetailsModel(ApplicationDbContext context) => _context = context;

        public User User { get; set; } = new();

        private bool IsAuthorized()
        {
            var role = HttpContext.Session.GetString("UserRole");
            return role == "Administrador";
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (!IsAuthorized())
                return RedirectToPage("/AccessDenied");

            User = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (User == null)
                return NotFound();

            return Page();
        }
    }
}
