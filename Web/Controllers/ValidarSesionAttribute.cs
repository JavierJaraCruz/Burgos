using System.Web;
using System.Web.Mvc;

namespace Web.Controllers // Asegúrate de que coincida con tu namespace
{
    public class ValidarSesionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            
            if (HttpContext.Current.Session["UsuarioId"] == null)
            {
               
                filterContext.Result = new RedirectResult("~/Login/Index");
            }

            base.OnActionExecuting(filterContext);
        }
    }
}