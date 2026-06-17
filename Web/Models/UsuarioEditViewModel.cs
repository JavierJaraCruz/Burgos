using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


    public class UsuarioEditViewModel
    {
        public int UsuarioId { get; set; }

        [Required, StringLength(50)]
        public string NombreUsuario { get; set; }

        [Required, EmailAddress, StringLength(100)]
        public string Email { get; set; }

        public bool Estado { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un rol para el usuario")]
        [Display(Name = "Rol asignado")]
        public int RolId { get; set; }
}
