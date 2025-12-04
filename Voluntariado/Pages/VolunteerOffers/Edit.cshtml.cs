using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Voluntariado.Data;
using Voluntariado.Models;

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

        private IActionResult? VerificarAcceso()
        {
            var role = HttpContext.Session.GetString("Role");
            if (string.IsNullOrEmpty(role))
                return RedirectToPage("/Auth/Login");

            if (role != "Ofertante")
            {
                TempData["ErrorMessage"] = "⚠️ Solo los ofertantes pueden editar sus ofertas.";
                return RedirectToPage("/Auth/Login");
            }

            return null;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var redirect = VerificarAcceso();
            if (redirect != null)
                return redirect;

            var userId = HttpContext.Session.GetString("UserId");
            Offer = await _context.VolunteerOffers.FirstOrDefaultAsync(o => o.Id == id && o.UserId.ToString() == userId);

            if (Offer == null)
                return RedirectToPage("Index");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var redirect = VerificarAcceso();
            if (redirect != null)
                return redirect;

            if (!ModelState.IsValid)
                return Page();

            var userId = HttpContext.Session.GetString("UserId");
            var existingOffer = await _context.VolunteerOffers.FirstOrDefaultAsync(o => o.Id == Offer.Id && o.UserId.ToString() == userId);

            if (existingOffer == null)
            {
                TempData["ErrorMessage"] = "❌ No puedes editar una oferta que no te pertenece.";
                return RedirectToPage("Index");
            }

            existingOffer.Title = Offer.Title;
            existingOffer.Description = Offer.Description;
            existingOffer.EmailContact = Offer.EmailContact;
            existingOffer.TotalSpots = Offer.TotalSpots;

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "✅ Oferta actualizada correctamente.";
            return RedirectToPage("Index");
        }
    }
}
