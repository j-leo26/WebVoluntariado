using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace Voluntariado.Helpers
{
    public class BasePageModel : PageModel
    {
        private readonly string[] _rolesPermitidos;

        public BasePageModel(params string[] rolesPermitidos)
        {
            _rolesPermitidos = rolesPermitidos;
        }

        public override void OnPageHandlerExecuting(Microsoft.AspNetCore.Mvc.Filters.PageHandlerExecutingContext context)
        {
            var role = context.HttpContext.Session.GetString("Role");

            // Si no ha iniciado sesión
            if (string.IsNullOrEmpty(role))
            {
                context.Result = new RedirectToPageResult("/Auth/Login", new { mensaje = "Debes iniciar sesión para continuar." });
                return;
            }

            // Si tiene sesión pero no el rol correcto
            if (_rolesPermitidos.Length > 0 && !_rolesPermitidos.Contains(role))
            {
                context.Result = new RedirectToPageResult("/Auth/Login", new { mensaje = "No tienes permisos para acceder a esta sección." });
                return;
            }

            base.OnPageHandlerExecuting(context);
        }
    }
}
