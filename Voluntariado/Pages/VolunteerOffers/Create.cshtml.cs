using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Voluntariado.Data;
using Voluntariado.Models;

namespace Voluntariado.Pages.VolunteerOffers
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

        private IActionResult? VerificarAcceso()
        {
            var role = HttpContext.Session.GetString("Role");
            if (string.IsNullOrEmpty(role))
                return RedirectToPage("/Auth/Login");

            if (role != "Ofertante")
            {
                TempData["ErrorMessage"] = "⚠️ Solo los ofertantes pueden crear ofertas.";
                return RedirectToPage("/Auth/Login");
            }

            return null;
        }

        public IActionResult OnGet()
        {
            var redirect = VerificarAcceso();
            if (redirect != null)
                return redirect;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var redirect = VerificarAcceso();
            if (redirect != null)
                return redirect;

            if (!ModelState.IsValid)
                return Page();

            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
            {
                TempData["ErrorMessage"] = "No se pudo identificar al usuario logueado.";
                return RedirectToPage("/Auth/Login");
            }

            Offer.UserId = userId;
            Offer.CreatedAt = DateTime.Now;
            Offer.ApplicantsCount = 0;

            _context.VolunteerOffers.Add(Offer);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "✅ Oferta creada correctamente.";
            return RedirectToPage("Index");
        }
    }
}
