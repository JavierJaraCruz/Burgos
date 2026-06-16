using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Web.ViewModels
{
    public class ProductoViewModel
    {
        public int ProductoId { get; set; }

    [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [StringLength(500)]
        public string Descripcion { get; set; }

        [Required]
        public decimal Precio { get; set; }

        [Required]
        public int Stock { get; set; }

        [Required]
        public int CategoriaId { get; set; }

        public string CategoriaNombre { get; set; }

        public string ImagenUrl { get; set; }

        public bool Activo { get; set; }

        public IEnumerable<SelectListItem> Categorias { get; set; }
    }


}
