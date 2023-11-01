using System;
using System.Collections.Generic;

namespace CanchaApp.Modelo;

public partial class Turno
{
    public int Id { get; set; }

    public TimeSpan? HoraInicio { get; set; }
}
