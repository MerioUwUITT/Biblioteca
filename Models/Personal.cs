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
    
    public partial class Personal
    {
        public Personal()
        {
            this.Checadas = new HashSet<Checada>();
            this.Prestamoes = new HashSet<Prestamo>();
            this.Ventas = new HashSet<Venta>();
        }
    
        public int id_personal { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string puesto { get; set; }
        public Nullable<decimal> sueldo { get; set; }
        public Nullable<bool> Activo { get; set; }
    
        public virtual ICollection<Checada> Checadas { get; set; }
        public virtual ICollection<Prestamo> Prestamoes { get; set; }
        public virtual ICollection<Venta> Ventas { get; set; }
    }
}
