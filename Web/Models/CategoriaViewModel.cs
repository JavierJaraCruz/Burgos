using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class CategoriaViewModel
    {
        public int CategoriaId { get; set; }


    [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100)]
        public string Nombre { get; set; }

        [StringLength(250)]
        public string Descripcion { get; set; }
    }


}
