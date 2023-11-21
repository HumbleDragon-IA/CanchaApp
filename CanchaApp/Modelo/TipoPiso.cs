using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CanchaApp.Modelo;

public partial class TipoPiso
{
    
    public int Id { get; set; }

    [Display(Name = "Tipo de Piso")]
    public string? TipoPiso1 { get; set; }

   

    public virtual ICollection<Cancha> Cancha { get; set; } = new List<Cancha>();

    

   

}
