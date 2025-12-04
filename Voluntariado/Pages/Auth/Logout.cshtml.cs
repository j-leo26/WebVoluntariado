using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Voluntariado.Pages.Auth
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnPost()
        {
            HttpContext.Session.Clear();
            TempData["SuccessMessage"] = "Has cerrado sesión correctamente.";
            return RedirectToPage("/Index");
        }
    }
}
