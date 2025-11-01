using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;

namespace Voluntariado.Helpers
{
    public static class AuthHelper
    {
        // Recibe HttpContext para no forzar Controller
        public static IActionResult? RedirectIfNotAuthorized(HttpContext httpContext, string[] rolesPermitidos)
        {
            var role = httpContext.Session.GetString("Role");

            if (string.IsNullOrEmpty(role))
            {
                // No ha iniciado sesión → ir al login (sin mensaje)
                return new RedirectToPageResult("/Auth/Login");
            }

            if (!rolesPermitidos.Contains(role))
            {
                // Guardar mensaje en session y redirigir al login
                var mensaje = "No tienes permiso para acceder a esta página, inicia sesión con un rol autorizado.";
                httpContext.Session.SetString("WarningMessage", mensaje);

                return new RedirectToPageResult("/Auth/Login");
            }

            return null;
        }
    }
}
