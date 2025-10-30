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

        public async Task OnGetAsync()
        {
            Roles = await _context.Roles.ToListAsync();
        }
    }
}
