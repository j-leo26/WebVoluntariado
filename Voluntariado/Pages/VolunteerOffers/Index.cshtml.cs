using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Voluntariado.Data;
using Voluntariado.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Voluntariado.Pages.Offers
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<VolunteerOffer> Offers { get; set; } = new List<VolunteerOffer>();

        public async Task OnGetAsync()
        {
            // Simulamos que el usuario 1 está logueado (Ofertante)
            int currentUserId = 1;

            Offers = await _context.VolunteerOffers
                .Include(o => o.User)
                .Where(o => o.UserId == currentUserId)
                .ToListAsync();
        }
    }
}
