using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Spoe.Models
{
	#region Registrousuario definition
	public class RegisterViewModel
    {
		//Datpos obligatorios para registro de usuario
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
		//error de cantidad de caracteres
        [StringLength(100, ErrorMessage = "La contraseña debe tener de 0-6 caracteres", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
		//error de concidencia de campos
        [Compare("Password", ErrorMessage = "Ambas contraseñas no coinciden")]
		//datos requeridos para el registro de usuario
        public string ConfirmPassword { get; set; }
		[Required]
		public string Nombres { get; set; }
		[Required]
		public string Apellidos { get; set; }
		[Required]
		public int Cedula { get; set; }
		[Required]
		public string Cargo { get; set; }
		[Required]
		public int FechaNacimiento { get; set; }
		[Required]
		public string TipoUsuario { get; set; }
		[Required]
		public DateTime Fecha { get; set; }
	}
	#endregion
}
