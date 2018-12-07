using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Spoe.Data.Migrations
{
	#region CreacionTablaEstudiante definition
	public partial class Tercera : Migration
    {
		protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estudiantes",
                columns: table => new
                {
					//Creacion de los campos para el registro de datos
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombres = table.Column<string>(nullable: true),
                    Apellidos = table.Column<string>(nullable: true),
                    GradoCursar = table.Column<int>(nullable: false),
                    NombreTutor = table.Column<string>(nullable: true),
                    TelefonoTutor = table.Column<int>(nullable: false),
                    Direccion = table.Column<string>(nullable: true),
                    Documentos = table.Column<string>(nullable: true),
                    Fecha = table.Column<DateTime>(nullable: false),
                    EmpleadoId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
					//Clave principal de la tabla
                    table.PrimaryKey("PK_Estudiantes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Estudiantes_AspNetUsers_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
			//relacion de registro de estudiantes con el usuario que los registra
            migrationBuilder.CreateIndex(
                name: "IX_Estudiantes_EmpleadoId",
                table: "Estudiantes",
                column: "EmpleadoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Estudiantes");
        }
    }
	#endregion
}
