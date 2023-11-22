using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CanchaApp.Modelo;

public partial class Cancha
{
    public int Id { get; set; }
    [Display(Name = "Tamaño")]
    public int IdCapacidad { get; set; }
    [Display(Name = "Tipo de Piso")]
    public int IdTipoPiso { get; set; }

    public double? Precio { get; set; }
    [Display(Name = "Tamaño")]
    public virtual Capacidad IdCapacidadNavigation { get; set; } = null!;
    [Display(Name = "Tipo de Piso")]
    public virtual TipoPiso IdTipoPisoNavigation { get; set; } = null!;

    public virtual ICollection<TurnoReservado> TurnoReservados { get; set; } = new List<TurnoReservado>();



    
    
}
