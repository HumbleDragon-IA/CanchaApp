using System;
using System.Collections.Generic;

namespace CanchaApp.Modelo;

public partial class Capacidad
{
    public int Id { get; set; }

    public int? Tamaño { get; set; }

    public virtual ICollection<Cancha> Cancha { get; set; } = new List<Cancha>();
}
