using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Voluntariado.Data;
using Voluntariado.Models;
using System.Threading.Tasks;

namespace Voluntariado.Pages.VolunteerOffers
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public VolunteerOffer Offer { get; set; } = new VolunteerOffer();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Offer = await _context.VolunteerOffers.FindAsync(id);

            if (Offer == null)
                return RedirectToPage("Index");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var offer = await _context.VolunteerOffers.FindAsync(id);

            if (offer != null)
            {
                _context.VolunteerOffers.Remove(offer);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("Index");
        }
    }
}
