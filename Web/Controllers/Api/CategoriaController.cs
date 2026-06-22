using Entities;
using Services;
using System.Collections.Generic;
using System.Web.Http;

namespace API.Controllers
{
    [RoutePrefix("api/categorias")]
    public class CategoriaController : ApiController
    {
        private readonly CategoriaService categoriaService = new CategoriaService();

        // GET api/categorias
        [HttpGet]
        [Route("")]
        public IHttpActionResult ListarCategorias()
        {
            List<Categoria> categorias = categoriaService.ListarCategorias();
            return Ok(categorias);
        }

        // GET api/categorias/5
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult ObtenerCategoria(int id)
        {
            Categoria categoria = categoriaService.ObtenerCategoria(id);

            if (categoria == null)
                return NotFound();

            return Ok(categoria);
        }
    }
}