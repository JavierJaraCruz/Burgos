using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CategoriaService
    {
        private readonly CategoriaDAL categoriaDAL = new CategoriaDAL();

        public int CrearCategoria(Categoria c) => categoriaDAL.Insertar(c);

        public Categoria ObtenerCategoria(int id) => categoriaDAL.ObtenerPorId(id);

        public List<Categoria> ListarCategorias() => categoriaDAL.Listar();

        public void ActualizarCategoria(Categoria c) => categoriaDAL.Actualizar(c);

        public void EliminarCategoria(int id) => categoriaDAL.Eliminar(id);
    }
}
