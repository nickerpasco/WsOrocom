//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace orocomWS.Models.Bd
{
    using System;
    using System.Collections.Generic;
    
    public partial class AsistenciaDiariaMarcas
    {
        public int Empleado { get; set; }
        public int Secuencia { get; set; }
        public Nullable<System.DateTime> FechaMarcacion { get; set; }
        public string TipoMarcacion { get; set; }
        public string LugarMarcacion { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public string Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string IpCreacion { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string Comentarios { get; set; }
    }
}
