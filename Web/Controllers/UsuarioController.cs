using Entities;
using Services;
using System;
using System.Net;
using System.Web.Mvc;
using Web.ViewModels; 

namespace Web.Controllers
{
    [ValidarSesion]
    public class UsuarioController : Controller
    {
        private readonly UsuarioService usuarioService = new UsuarioService();

        // GET: Usuario
        public ActionResult Index() => View(usuarioService.ListarUsuarios());

        // GET: Usuario/Detalle/5

        // GET: Usuario/Detalle/5
        public ActionResult Detalle(int id)
        {
            var usuario = usuarioService.ObtenerUsuario(id);
            if (usuario == null) return HttpNotFound();

            ViewBag.RolNombre = usuarioService.ObtenerNombreRolPorUsuario(id);

            return View(usuario);
        }
        // GET: Usuario/Crear
        [HttpGet]
        public ActionResult Crear()
        {
          
            ViewBag.Roles = new SelectList(usuarioService.ListarRoles(), "RolId", "NombreRol");
            return View();
        }

        // POST: Usuario/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Crear(UsuarioEditViewModel model)
        {
            
            if (ModelState.IsValid)
            {
                var salt = PasswordHelper.GenerarSalt();

                
                var hash = PasswordHelper.GenerarPasswordHash("default123", salt);

                var usuario = new Usuario
                {
                    NombreUsuario = model.NombreUsuario,
                    Email = model.Email,
                    Estado = model.Estado,
                    PasswordHash = hash,
                    Salt = salt,
                    FechaRegistro = DateTime.Now
                };

               
                int nuevoUsuarioId = usuarioService.CrearUsuario(usuario);

            
                usuarioService.AsignarRolAUsuario(nuevoUsuarioId, model.RolId);

              
                TempData["SuccessMessage"] = "Usuario creado con éxito. La contraseña inicial es: default123";
                return RedirectToAction("Index");
            }

           
            ViewBag.Roles = new SelectList(usuarioService.ListarRoles(), "RolId", "NombreRol");
            return View(model);
        }

        // GET: Usuario/Editar/5
        [HttpGet]
        public ActionResult Editar(int id) => View(usuarioService.ObtenerUsuario(id));

        // POST: Usuario/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(UsuarioEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var usuario = usuarioService.ObtenerUsuario(model.UsuarioId);
                if (usuario == null) return HttpNotFound();

                usuario.NombreUsuario = model.NombreUsuario;
                usuario.Email = model.Email;
                usuario.Estado = model.Estado;
                usuarioService.ActualizarUsuario(usuario);

                TempData["SuccessMessage"] = "Usuario actualizado correctamente.";
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Usuario/Delete/5
        public ActionResult Delete(int id)
        {
            if (id <= 0) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var usuario = usuarioService.ObtenerUsuario(id);
            if (usuario == null) return HttpNotFound();

            return View(usuario);
        }

        // POST: Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var usuario = usuarioService.ObtenerUsuario(id);
            if (usuario == null) return HttpNotFound();

            usuarioService.EliminarUsuario(id);
            TempData["SuccessMessage"] = "Usuario eliminado correctamente.";
            return RedirectToAction("Index");
        }

    }
}