using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntegradorNET.Migrations
{
    public partial class DbSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Proyectos",
                columns: table => new
                {
                    codProyecto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "VARCHAR(200)", nullable: false),
                    direccion = table.Column<string>(type: "VARCHAR(200)", nullable: false),
                    estado = table.Column<int>(type: "int", nullable: false),
                    eliminado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proyectos", x => x.codProyecto);
                });

            migrationBuilder.CreateTable(
                name: "Servicios",
                columns: table => new
                {
                    codServicio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descr = table.Column<string>(type: "VARCHAR(200)", nullable: false),
                    estado = table.Column<int>(type: "int", nullable: false),
                    valorHora = table.Column<decimal>(type: "DECIMAL(19,4)", nullable: false),
                    eliminado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicios", x => x.codServicio);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    codUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    dni = table.Column<int>(type: "int", nullable: false),
                    tipo = table.Column<int>(type: "int", nullable: false),
                    contrasena = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    eliminado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.codUsuario);
                });

            migrationBuilder.CreateTable(
                name: "Trabajos",
                columns: table => new
                {
                    codTrabajo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fecha = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    codProyecto = table.Column<int>(type: "int", nullable: false),
                    codServicio = table.Column<int>(type: "int", nullable: false),
                    cantHoras = table.Column<int>(type: "int", nullable: false),
                    valorHora = table.Column<decimal>(type: "DECIMAL(19,4)", nullable: false),
                    costo = table.Column<decimal>(type: "DECIMAL(19,4)", nullable: false),
                    eliminado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trabajos", x => x.codTrabajo);
                    table.ForeignKey(
                        name: "FK_Trabajos_Proyectos_codProyecto",
                        column: x => x.codProyecto,
                        principalTable: "Proyectos",
                        principalColumn: "codProyecto",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trabajos_Servicios_codServicio",
                        column: x => x.codServicio,
                        principalTable: "Servicios",
                        principalColumn: "codServicio",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trabajos_codProyecto",
                table: "Trabajos",
                column: "codProyecto");

            migrationBuilder.CreateIndex(
                name: "IX_Trabajos_codServicio",
                table: "Trabajos",
                column: "codServicio");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trabajos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Proyectos");

            migrationBuilder.DropTable(
                name: "Servicios");
        }
    }
}
