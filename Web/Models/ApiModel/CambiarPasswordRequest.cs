using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Model.ApiModel
{
    public class CambiarPasswordRequest
    {
        public string NombreUsuario { get; set; }

        public string PasswordActual { get; set; }

        public string PasswordNueva { get; set; }
    }
}