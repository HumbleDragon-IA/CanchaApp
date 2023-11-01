using System;
using System.Collections.Generic;

namespace CanchaApp.Modelo;

public partial class ComentarioUsuario
{
    public int IdUser { get; set; }

    public int IdComentario { get; set; }

    public virtual Usuario IdUser1 { get; set; } = null!;

    public virtual Comentario IdUserNavigation { get; set; } = null!;
}
