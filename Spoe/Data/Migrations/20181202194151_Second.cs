using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Spoe.Data.Migrations
{
	#region RegistroAlmacenamientoDatosEmpleados definition
	public partial class Second : Migration
    {
		#region RegistroDatosEmpleados definition
		protected override void Up(MigrationBuilder migrationBuilder)
		{ 
			//Insercion de datospara registro de Empleados
            migrationBuilder.AddColumn<string>(
                name: "Apellidos",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cargo",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Cedula",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Fecha",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "FechaNacimiento",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Nombres",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipoUsuario",
                table: "AspNetUsers",
                nullable: true);
        }
		#endregion

		#region AlmacenamientoDatosEmpleados definition
		protected override void Down(MigrationBuilder migrationBuilder)
        {
			//almacenamiento de datos de empleados en la tabla
            migrationBuilder.DropColumn(
                name: "Apellidos",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Cargo",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Cedula",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Fecha",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FechaNacimiento",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Nombres",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TipoUsuario",
                table: "AspNetUsers");
        }
		#endregion
	}
	#endregion
}
