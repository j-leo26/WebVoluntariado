using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Voluntariado.Data;
using Voluntariado.Models;

namespace Voluntariado.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public IndexModel(ApplicationDbContext context) => _context = context;

        public IList<User> Users { get; set; } = new List<User>();

        private bool IsAuthorized()
        {
            var role = HttpContext.Session.GetString("Role");
            return role == "Administrador";
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!IsAuthorized())
                return RedirectToPage("/AccessDenied");

            Users = await _context.Users.Include(u => u.Role).ToListAsync();
            return Page();
        }
    }
}
