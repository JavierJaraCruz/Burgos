using Entities;
using Services;
using System;
using System.Net;
using System.Reflection;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioService usuarioService = new UsuarioService();

        public ActionResult Index() => View(usuarioService.ListarUsuarios());

        public ActionResult Detalle(int id) => View(usuarioService.ObtenerUsuario(id));

        [HttpGet]
        public ActionResult Crear() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UsuarioEditViewModel model)
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

                usuarioService.CrearUsuario(usuario);

                TempData["SuccessMessage"] = "Usuario creado correctamente.";
                return RedirectToAction("Index");
            }
            return View(model);
        }


        [HttpGet]
        public ActionResult Editar(int id) => View(usuarioService.ObtenerUsuario(id));

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
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var usuario = usuarioService.ObtenerUsuario(id);
            if (usuario == null) return HttpNotFound();

            return View(usuario);
        }

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