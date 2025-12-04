using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Voluntariado.Data;

namespace Voluntariado.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public List<string> RolesLabels { get; set; } = new();
        public List<int> RolesData { get; set; } = new();

        public List<string> OfertasLabels { get; set; } = new();
        public List<int> OfertasData { get; set; } = new();

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            var redirect = VerificarAcceso();
            if (redirect != null)
                return redirect;

            CargarEstadisticas();
            return Page();
        }

        private void CargarEstadisticas()
        {
            // ===================== USUARIOS POR ROL =====================
            RolesLabels = _context.Roles
                .Select(r => r.Name)
                .ToList();

            RolesData = RolesLabels
                .Select(rol =>
                    _context.Users.Count(u => u.Role.Name == rol)
                ).ToList();

            // ===================== OFERTAS POR MES =====================
            var ofertasPorMes = _context.VolunteerOffers
                .GroupBy(o => new { o.CreatedAt.Month })
                .OrderBy(g => g.Key.Month)
                .Select(g => new
                {
                    Mes = g.Key.Month,
                    Cantidad = g.Count()
                })
                .ToList();

            OfertasLabels = ofertasPorMes.Select(o =>
                new DateTime(2025, o.Mes, 1).ToString("MMM")
            ).ToList();

            OfertasData = ofertasPorMes.Select(o => o.Cantidad).ToList();
        }

        private IActionResult? VerificarAcceso()
        {
            var role = HttpContext.Session.GetString("Role");

            if (string.IsNullOrEmpty(role))
                return RedirectToPage("/Auth/Login");

            if (role != "Administrador")
            {
                TempData["ErrorMessage"] = "⚠️ No tienes permiso para acceder a esta página.";
                return RedirectToPage("/Auth/Login");
            }

            return null;
        }
    }
}
