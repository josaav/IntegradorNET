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
                    eliminado = table.Column<int>(type: "int", nullable: false),
                    valorHora = table.Column<decimal>(type: "DECIMAL(19,2)", nullable: false)
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
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tipo = table.Column<int>(type: "int", nullable: false),
                    contrasena = table.Column<string>(type: "VARCHAR(100)", nullable: false),
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
                    fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    codProyecto = table.Column<int>(type: "int", nullable: false),
                    codServicio = table.Column<int>(type: "int", nullable: false),
                    cantHoras = table.Column<int>(type: "int", nullable: false),
                    valorHora = table.Column<decimal>(type: "DECIMAL(19,2)", nullable: false),
                    costo = table.Column<decimal>(type: "DECIMAL(19,2)", nullable: false),
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

            migrationBuilder.InsertData(
                table: "Proyectos",
                columns: new[] { "codProyecto", "direccion", "eliminado", "estado", "nombre" },
                values: new object[,]
                {
                    { 1, "Sede Lima", 0, 2, "Proyecto Cuenca Andina" },
                    { 2, "Sede Tucumán", 0, 3, "Proyecto Cuenca Noroeste Argentino" },
                    { 3, "Sede Tucumán", 0, 2, "Proyecto Cuenca Marina Austral" },
                    { 4, "Sede Neuquén", 0, 3, "Proyecto Cuenca Neuquina" },
                    { 5, "Sede Barranquilla", 0, 1, "Proyecto Gas Natural Cuenca Magdalena" }
                });

            migrationBuilder.InsertData(
                table: "Servicios",
                columns: new[] { "codServicio", "descr", "eliminado", "estado", "valorHora" },
                values: new object[,]
                {
                    { 1, "Exploración Geofísica", 0, 1, 500m },
                    { 2, "Peforación Direccional y Horizontal", 0, 1, 1000m },
                    { 3, "Ingeniería de Reservorios", 0, 0, 750m },
                    { 4, "Transporte y Logística", 0, 1, 500m },
                    { 5, "Mantenimiento de Plataformas Offshore", 0, 1, 1500m }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "codUsuario", "contrasena", "dni", "eliminado", "email", "nombre", "tipo" },
                values: new object[,]
                {
                    { 1, "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4", 26175345, 0, "jperez@techoil.com", "Juan Pérez", 1 },
                    { 2, "ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f", 36565454, 0, "jgomez@techoil.com", "José Gómez", 2 },
                    { 3, "7d1a54127b222502f5b79b5fb0803061152a44f92b37e23c6527baf665d4da9a", 32115082, 0, "malvarez@techoil.com", "María Álvarez", 2 }
                });

            migrationBuilder.InsertData(
                table: "Trabajos",
                columns: new[] { "codTrabajo", "cantHoras", "costo", "eliminado", "fecha", "codProyecto", "codServicio", "valorHora" },
                values: new object[,]
                {
                    { 1, 35, 10500m, 0, new DateTime(2023, 9, 24, 18, 21, 25, 69, DateTimeKind.Local).AddTicks(1450), 1, 2, 300m },
                    { 2, 48, 19200m, 0, new DateTime(2023, 9, 24, 18, 21, 25, 69, DateTimeKind.Local).AddTicks(1480), 4, 5, 400m },
                    { 3, 35, 11200m, 0, new DateTime(2023, 9, 24, 18, 21, 25, 69, DateTimeKind.Local).AddTicks(1480), 4, 2, 320m },
                    { 4, 60, 12000m, 0, new DateTime(2023, 9, 24, 18, 21, 25, 69, DateTimeKind.Local).AddTicks(1480), 2, 3, 200m },
                    { 5, 35, 15750m, 0, new DateTime(2023, 9, 24, 18, 21, 25, 69, DateTimeKind.Local).AddTicks(1480), 1, 1, 450m },
                    { 6, 50, 15000m, 0, new DateTime(2023, 9, 24, 18, 21, 25, 69, DateTimeKind.Local).AddTicks(1480), 3, 1, 300m },
                    { 7, 45, 18500m, 0, new DateTime(2023, 9, 24, 18, 21, 25, 69, DateTimeKind.Local).AddTicks(1490), 5, 4, 400m }
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
