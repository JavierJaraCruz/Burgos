using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class ProveedorViewModel
    {
        public int ProveedorId { get; set; }


    [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100)]
        public string Nombre { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(20)]
        public string Telefono { get; set; }

        [StringLength(200)]
        public string Direccion { get; set; }
    }


}
