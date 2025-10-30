using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Voluntariado.Data;
using Voluntariado.Models;
using System.Threading.Tasks;

namespace Voluntariado.Pages.VolunteerOffers
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            _context.Attach(Offer).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
