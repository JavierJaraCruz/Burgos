using System;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
public class KardexViewModel
{
public int ProductoId { get; set; }

    [Display(Name = "Fecha")]
    public DateTime Fecha { get; set; }

    [Display(Name = "Tipo Movimiento")]
    public string TipoMovimiento { get; set; }

    [Display(Name = "Cantidad")]
    public int Cantidad { get; set; }

    [Display(Name = "Referencia")]
    public string Referencia { get; set; }

    [Display(Name = "Saldo")]
    public int Saldo { get; set; }
}

}
