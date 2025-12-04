using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Voluntariado.Data;
using Voluntariado.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;


namespace Voluntariado.Pages.VolunteerOffers
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<VolunteerOffer> Offers { get; set; } = new List<VolunteerOffer>();

        private IActionResult? VerificarAcceso()
        {
            var role = HttpContext.Session.GetString("Role");

            if (string.IsNullOrEmpty(role))
                return RedirectToPage("/Auth/Login");

            if (role != "Ofertante")
            {
                TempData["ErrorMessage"] = "⚠️ Solo los ofertantes pueden ver sus ofertas.";
                return RedirectToPage("/Auth/Login");
            }

            return null;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var redirect = VerificarAcceso();
            if (redirect != null)
                return redirect;

            // ✅ Obtener el ID del usuario desde la sesión
            var currentUserIdString = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(currentUserIdString) || !int.TryParse(currentUserIdString, out int currentUserId))
            {
                TempData["ErrorMessage"] = "No se pudo identificar el usuario logueado (sesión expirada o inválida).";
                return RedirectToPage("/Auth/Login");
            }

            // 🔍 Cargar solo las ofertas creadas por ese usuario
            Offers = await _context.VolunteerOffers
                .Include(o => o.User)
                .Where(o => o.UserId == currentUserId)
                .ToListAsync();

            return Page();
        }

    }

}
