using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Spoe.Data
{
	#region ManejoBaseDeDatos definition
	public class ApplicationDbContext : IdentityDbContext<Empleado>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{}
		public DbSet<Estudiante> Estudiantes { get; set; }
	}
	#endregion
}
