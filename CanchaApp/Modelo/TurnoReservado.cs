using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;


namespace CanchaApp.Modelo;

public partial class TurnoReservado
{
    public int Id { get; set; }

    public int IdUsuario { get; set; }

    public int IdTurno { get; set; }

    public int IdCancha { get; set; }

    public virtual Cancha IdCanchaNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

    public virtual Turno IdTurnoNavigation { get; set; } = null!;

}
