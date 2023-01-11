using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace orocomWS.Models
{

	public class ModelAsistencia
	{
		public string nombrecompleto { get; set; }
		public string companiasocio { get; set; }
		public string sucursal { get; set; }
		public string estado { get; set; }
		public string codigocarnet { get; set; }
		public string area_desc { get; set; }
		public int Empleado { get; set; }
		public int Secuencia { get; set; }
		public DateTime? FechaMarcacion { get; set; }
		public string TipoMarcacion { get; set; }
		public string LugarMarcacion { get; set; }
		public string Latitud { get; set; }
		public string Longitud { get; set; }
		public string Estado { get; set; }
		public string UsuarioCreacion { get; set; }
		public string IpCreacion { get; set; }
		public DateTime? FechaCreacion { get; set; }
	}
}