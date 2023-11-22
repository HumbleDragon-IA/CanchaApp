using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CanchaApp.Modelo;

public partial class TurnoReservado
{
   
    public int Id { get; set; }

    [Display(Name = "Usuario")]
    public int IdUsuario { get; set; }

    [Display(Name = "Horario")]
    public int IdTurno { get; set; }

    [Display(Name = "Cancha")]
    public int IdCancha { get; set; }

    [Display(Name = "Cancha")]
    public virtual Cancha IdCanchaNavigation { get; set; } = null!;

    [Display(Name = "Usuario")]
    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

    
    
}
