using System;
using System.Collections.Generic;

namespace CanchaApp.Modelo;

public partial class Usuario
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Password { get; set; }

    public bool? Admin { get; set; }

    public virtual ICollection<ComentarioUsuario> ComentarioUsuarios { get; set; } = new List<ComentarioUsuario>();

    public virtual ICollection<TurnoReservado> TurnoReservados { get; set; } = new List<TurnoReservado>();
}
