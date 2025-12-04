using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Voluntariado.Data;
using Voluntariado.Models;

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

        private IActionResult? VerificarAcceso()
        {
            var role = HttpContext.Session.GetString("Role");
            if (string.IsNullOrEmpty(role))
                return RedirectToPage("/Auth/Login");

            if (role != "Ofertante")
            {
                TempData["ErrorMessage"] = "⚠️ Solo los ofertantes pueden ver los detalles de sus ofertas.";
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
            Offer = await _context.VolunteerOffers
                .Include(o => o.User)
                .FirstOrDefaultAsync(o => o.Id == id && o.UserId.ToString() == userId);

            if (Offer == null)
                return RedirectToPage("Index");

            return Page();
        }
    }
}
