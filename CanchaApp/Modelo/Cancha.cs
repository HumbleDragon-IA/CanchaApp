using System;
using System.Collections.Generic;

namespace CanchaApp.Modelo;

public partial class Cancha
{
    public int Id { get; set; }

    public int IdCapacidad { get; set; }

    public int IdTipoPiso { get; set; }

    public double? Precio { get; set; }

    public virtual Capacidad IdCapacidadNavigation { get; set; } = null!;

    public virtual TipoPiso IdTipoPisoNavigation { get; set; } = null!;

    public virtual ICollection<TurnoReservado> TurnoReservados { get; set; } = new List<TurnoReservado>();
}
