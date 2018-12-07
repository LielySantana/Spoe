using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Spoe.Data
{
	#region RegistroEmpleado Definition
	public class Empleado : IdentityUser
	{
		public Empleado()
		{
			//Modelo de encriptacion
			Estudiantes = new HashSet<Estudiante>();
		}
		//Datos correspondientes para el registro de usuarios
		public string Nombres { get; set; }
		public string Apellidos { get; set; }
		public int Cedula { get; set; }
		public string Cargo { get; set; }
		public int FechaNacimiento { get; set; }
		public string TipoUsuario { get; set; }
		public DateTime Fecha { get; set; }

		//manejo de datos de empleados
		public virtual ICollection<Estudiante> Estudiantes { get; set; }
	}
	#endregion
}

