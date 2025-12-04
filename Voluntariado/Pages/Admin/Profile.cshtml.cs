using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Voluntariado.Pages.Admin
{
    public class ProfileModel : PageModel
    {
        public string Nombre { get; private set; } = "";
        public string Correo { get; private set; } = "";
        public string UltimoAcceso { get; private set; } = "";

        public void OnGet()
        {
            Nombre = HttpContext.Session.GetString("UserName") ?? "Administrador";
            Correo = HttpContext.Session.GetString("UserEmail") ?? "admin@correo.com";
            UltimoAcceso = DateTime.Now.ToString("dd/MM/yyyy - hh:mm tt");
        }
    }
}
