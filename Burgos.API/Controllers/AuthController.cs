
using Burgos.API.Models;
using Entities;
using Services;
using System.Web.Http;

namespace API.Controllers
{
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        private readonly UsuarioService usuarioService = new UsuarioService();

        // POST api/auth/login
        [HttpPost]
        [Route("login")]
        public IHttpActionResult Login(LoginRequest request)
        {
            if (request == null)
                return BadRequest();

            Usuario usuario =
                usuarioService.ObtenerUsuarioPorNombre(request.NombreUsuario);

            if (usuario == null)
            {
                return Ok(new
                {
                    Success = false,
                    Message = "Usuario no encontrado"
                });
            }

            string hashIngresado =
                PasswordHelper.GenerarPasswordHash(
                    request.Password,
                    usuario.Salt
                );

            if (hashIngresado != usuario.PasswordHash)
            {
                return Ok(new
                {
                    Success = false,
                    Message = "Contraseña incorrecta"
                });
            }

            return Ok(new
            {
                Success = true,
                UsuarioId = usuario.UsuarioId,
                NombreUsuario = usuario.NombreUsuario,
                Rol = usuarioService.ObtenerNombreRolPorUsuario(usuario.UsuarioId)
            });
        }

        // POST api/auth/cambiar-password
        [HttpPost]
        [Route("cambiar-password")]
        public IHttpActionResult CambiarPassword(
            CambiarPasswordRequest request)
        {
            Usuario usuario =
                usuarioService.ObtenerUsuarioPorNombre(
                    request.NombreUsuario);

            if (usuario == null)
            {
                return Ok(new
                {
                    Success = false,
                    Message = "Usuario no encontrado"
                });
            }

            string hashActual =
                PasswordHelper.GenerarPasswordHash(
                    request.PasswordActual,
                    usuario.Salt);

            if (hashActual != usuario.PasswordHash)
            {
                return Ok(new
                {
                    Success = false,
                    Message = "La contraseña actual es incorrecta"
                });
            }

            string nuevoSalt =
                PasswordHelper.GenerarSalt();

            string nuevoHash =
                PasswordHelper.GenerarPasswordHash(
                    request.PasswordNueva,
                    nuevoSalt);

            usuario.Salt = nuevoSalt;
            usuario.PasswordHash = nuevoHash;

            usuarioService.ActualizarUsuario(usuario);

            return Ok(new
            {
                Success = true,
                Message = "Contraseña actualizada correctamente"
            });
        }
    }
}