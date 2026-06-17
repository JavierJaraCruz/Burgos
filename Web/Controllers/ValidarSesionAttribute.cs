using System.Web;
using System.Web.Mvc;

namespace Web.Controllers // Asegúrate de que coincida con tu namespace
{
    public class ValidarSesionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Si la sesión del UsuarioId es nula, significa que no se ha logueado
            if (HttpContext.Current.Session["UsuarioId"] == null)
            {
                // Lo redirigimos de patitas a la calle (al formulario de Login)
                filterContext.Result = new RedirectResult("~/Login/Index");
            }

            base.OnActionExecuting(filterContext);
        }
    }
}