using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spoe.Data
{
	#region RegistroEstudiante Definition
	public class Estudiante
	{
		//Campos correspondientes para el registro de estudiantes en el colegio
		public int Id { get; set; }
		public string Nombres { get; set; }
		public string Apellidos { get; set; }
		public int GradoCursar { get; set; }
		public string NombreTutor { get; set; }
		public int TelefonoTutor { get; set; }
		public string Direccion { get; set; }
		public string Documentos { get; set; }
		public DateTime Fecha { get; set; }

		public string EmpleadoId { get; set; }
		public Empleado Empleado { get; set; }
	}
	#endregion
}
