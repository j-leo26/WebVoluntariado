using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Voluntariado.Data;
using Voluntariado.Models;
using System.Threading.Tasks;

namespace Voluntariado.Pages.Offers
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public VolunteerOffer Offer { get; set; } = new VolunteerOffer();

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            // Simulamos que el usuario 1 está logueado
            Offer.UserId = 1;

            _context.VolunteerOffers.Add(Offer);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
