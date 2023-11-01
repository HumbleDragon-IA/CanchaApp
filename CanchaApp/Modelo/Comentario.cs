using System;
using System.Collections.Generic;

namespace CanchaApp.Modelo;

public partial class Comentario
{
    public int Id { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<ComentarioUsuario> ComentarioUsuario { get; set; } = new List<ComentarioUsuario>();
}
