using System;

namespace Spoe.Models
{
	#region CreacionVentanaErrores
	public class ErrorViewModel
	{
		public string RequestId { get; set; }

		public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
	}
	#endregion
}