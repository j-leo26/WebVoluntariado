using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Voluntariado.Data;
using Voluntariado.Models;

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

        private IActionResult? VerificarAcceso()
        {
            var role = HttpContext.Session.GetString("Role");
            if (string.IsNullOrEmpty(role))
                return RedirectToPage("/Auth/Login");

            if (role != "Ofertante")
            {
                TempData["ErrorMessage"] = "⚠️ Solo los ofertantes pueden eliminar ofertas.";
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

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var redirect = VerificarAcceso();
            if (redirect != null)
                return redirect;

            var userId = HttpContext.Session.GetString("UserId");
            var offer = await _context.VolunteerOffers.FirstOrDefaultAsync(o => o.Id == id && o.UserId.ToString() == userId);

            if (offer != null)
            {
                _context.VolunteerOffers.Remove(offer);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "✅ Oferta eliminada correctamente.";
            }

            return RedirectToPage("Index");
        }
    }
}
