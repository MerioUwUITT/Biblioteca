//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Biblioteca.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Libro
    {
        public Libro()
        {
            this.PrestamoLibroes = new HashSet<PrestamoLibro>();
            this.VentaLibroes = new HashSet<VentaLibro>();
        }
    
        public int id_libro { get; set; }
        public int id_categoria_libro { get; set; }
        public string titulo { get; set; }
        public string autor { get; set; }
        public string editorial { get; set; }
        public Nullable<int> numero_copias { get; set; }
        public Nullable<decimal> precio { get; set; }
        public Nullable<bool> Activo { get; set; }
    
        public virtual Categoria_libro Categoria_libro { get; set; }
        public virtual ICollection<PrestamoLibro> PrestamoLibroes { get; set; }
        public virtual ICollection<VentaLibro> VentaLibroes { get; set; }
    }
}
