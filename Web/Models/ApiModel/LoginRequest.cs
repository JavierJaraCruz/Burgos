using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Model.ApiModel
{
    public class LoginRequest
    {
        public string NombreUsuario { get; set; }
        public string Password { get; set; }
    }
}