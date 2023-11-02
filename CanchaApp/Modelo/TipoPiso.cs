using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;

namespace CanchaApp.Modelo;

public partial class TipoPiso
{
    
    public int Id { get; set; }

    public string? TipoPiso1 { get; set; }

   

    public virtual ICollection<Cancha> Cancha { get; set; } = new List<Cancha>();

    public TipoPisoEnum obtenerPisoXIndice()
    {
        TipoPisoEnum[] valores = (TipoPisoEnum[])Enum.GetValues(typeof(TipoPisoEnum));

        return valores[this.Id-1];
    }

   

}
