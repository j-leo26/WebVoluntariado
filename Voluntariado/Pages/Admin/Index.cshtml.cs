using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Voluntariado.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private IActionResult? VerificarAcceso()
        {
            var role = HttpContext.Session.GetString("Role");

            // No ha iniciado sesión → redirigir al login
            if (string.IsNullOrEmpty(role))
                return RedirectToPage("/Auth/Login");

            // No es Administrador → redirigir con mensaje de error
            if (role != "Administrador")
            {
                TempData["ErrorMessage"] = "⚠️ No tienes permiso para acceder a esta página, inicia sesión con un rol autorizado.";
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
    }
}
