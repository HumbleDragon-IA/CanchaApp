using System;
using System.Collections.Generic;

namespace CanchaApp.Modelo;

public partial class TipoPiso
{
    public int Id { get; set; }

    public string? TipoPiso1 { get; set; }

    public virtual ICollection<Cancha> Canchas { get; set; } = new List<Cancha>();
}
