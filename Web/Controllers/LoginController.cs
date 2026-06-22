using Entities;
using Services;
using System.Web.Mvc;
using System.Web.Security;
using Web.ViewModels;

namespace Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly UsuarioService usuarioService = new UsuarioService();

        [HttpGet]
        public ActionResult Index()
        {
            return View(); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                var usuario = usuarioService.ObtenerUsuarioPorNombre(model.NombreUsuario);

                if (usuario != null)
                {
                   
                    var hashIntento = PasswordHelper.GenerarPasswordHash(model.Password, usuario.Salt);

                    if (usuario.PasswordHash == hashIntento)
                    {
                        if (!usuario.Estado)
                        {
                            ModelState.AddModelError("", "Tu cuenta se encuentra inactiva. Contacta al administrador.");
                            return View("Index", model); 
                        }

                        
                        string nombreRol = usuarioService.ObtenerNombreRolPorUsuario(usuario.UsuarioId);

                        if (nombreRol != "Administrador" && nombreRol != "Admin")
                        {
                            ModelState.AddModelError("", "Acceso denegado: Solo los usuarios con rol Administrador pueden ingresar al sistema.");
                            return View("Index", model); 
                        }

                     
                        Session["UsuarioId"] = usuario.UsuarioId;
                        Session["NombreUsuario"] = usuario.NombreUsuario;
                        Session["Rol"] = nombreRol;

                        return RedirectToAction("Index", "Home");
                    }
                }

                
                ModelState.AddModelError("", "Usuario o contraseña incorrectos.");
            }

           
            return View("Index", model);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Index");
        }
    }
}