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
            return View(); // Busca Views/Login/Index.cshtml (Todo bien aquí)
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // 1. Buscamos al usuario por su nombre
                var usuario = usuarioService.ObtenerUsuarioPorNombre(model.NombreUsuario);

                if (usuario != null)
                {
                    // 2. Validamos la contraseña usando tu PasswordHelper
                    var hashIntento = PasswordHelper.GenerarPasswordHash(model.Password, usuario.Salt);

                    if (usuario.PasswordHash == hashIntento)
                    {
                        if (!usuario.Estado)
                        {
                            ModelState.AddModelError("", "Tu cuenta se encuentra inactiva. Contacta al administrador.");
                            return View("Index", model); // 👈 CORREGIDO: Fuerza a usar la vista "Index"
                        }

                        // 3. Validamos el Rol del usuario
                        string nombreRol = usuarioService.ObtenerNombreRolPorUsuario(usuario.UsuarioId);

                        if (nombreRol != "Administrador" && nombreRol != "Admin")
                        {
                            ModelState.AddModelError("", "Acceso denegado: Solo los usuarios con rol Administrador pueden ingresar al sistema.");
                            return View("Index", model); // 👈 CORREGIDO: Fuerza a usar la vista "Index"
                        }

                        // 4. Creamos la sesión
                        Session["UsuarioId"] = usuario.UsuarioId;
                        Session["NombreUsuario"] = usuario.NombreUsuario;
                        Session["Rol"] = nombreRol;

                        return RedirectToAction("Index", "Home");
                    }
                }

                // Error si las credenciales no coinciden
                ModelState.AddModelError("", "Usuario o contraseña incorrectos.");
            }

            // 👈 CORREGIDO: Si el ModelState no es válido o falló el login, regresa a la vista "Index"
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