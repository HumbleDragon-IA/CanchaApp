using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CanchaApp.Modelo;

public partial class VistaGeneralTurnosReservado
{
    
    public int  idVista { get; set; }
    
    public string? NombreUsuario { get; set; }

    public string? ApellidoUsuario { get; set; }

    public TimeSpan? HorarioReserva { get; set; }

    public string? TipoPisoCancha { get; set; }

    public int? TamañoCancha { get; set; }

    public double? Precio { get; set; }
}
