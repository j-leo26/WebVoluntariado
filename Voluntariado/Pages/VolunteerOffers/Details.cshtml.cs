using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Voluntariado.Data;
using Voluntariado.Models;
using System.Threading.Tasks;

namespace Voluntariado.Pages.VolunteerOffers
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public VolunteerOffer Offer { get; set; } = new VolunteerOffer();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Offer = await _context.VolunteerOffers
                .Include(o => o.User)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (Offer == null)
                return RedirectToPage("Index");

            return Page();
        }
    }
}
