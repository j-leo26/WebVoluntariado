using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Voluntariado.Pages.Admin
{
    public class SettingsModel : PageModel
    {
        [BindProperty] public string Nombre { get; set; } = "";
        [BindProperty] public string Correo { get; set; } = "";
        [BindProperty] public string Password { get; set; } = "";

        public void OnGet()
        {
            Nombre = HttpContext.Session.GetString("UserName") ?? "Administrador";
            Correo = HttpContext.Session.GetString("UserEmail") ?? "admin@correo.com";
        }

        public IActionResult OnPost()
        {
            HttpContext.Session.SetString("UserName", Nombre);
            HttpContext.Session.SetString("UserEmail", Correo);

            TempData["Success"] = "Configuración actualizada correctamente.";

            return RedirectToPage("/Admin/Profile");
        }
    }
}
