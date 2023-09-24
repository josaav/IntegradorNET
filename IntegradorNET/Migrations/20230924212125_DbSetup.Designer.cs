﻿// <auto-generated />
using System;
using IntegradorNET.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace IntegradorNET.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230924212125_DbSetup")]
    partial class DbSetup
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("IntegradorNET.Entities.Proyecto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("codProyecto");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("VARCHAR(200)")
                        .HasColumnName("direccion");

                    b.Property<int>("Eliminado")
                        .HasColumnType("int")
                        .HasColumnName("eliminado");

                    b.Property<int>("Estado")
                        .HasColumnType("int")
                        .HasColumnName("estado");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("VARCHAR(200)")
                        .HasColumnName("nombre");

                    b.HasKey("Id");

                    b.ToTable("Proyectos");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Direccion = "Sede Lima",
                            Eliminado = 0,
                            Estado = 2,
                            Nombre = "Proyecto Cuenca Andina"
                        },
                        new
                        {
                            Id = 2,
                            Direccion = "Sede Tucumán",
                            Eliminado = 0,
                            Estado = 3,
                            Nombre = "Proyecto Cuenca Noroeste Argentino"
                        },
                        new
                        {
                            Id = 3,
                            Direccion = "Sede Tucumán",
                            Eliminado = 0,
                            Estado = 2,
                            Nombre = "Proyecto Cuenca Marina Austral"
                        },
                        new
                        {
                            Id = 4,
                            Direccion = "Sede Neuquén",
                            Eliminado = 0,
                            Estado = 3,
                            Nombre = "Proyecto Cuenca Neuquina"
                        },
                        new
                        {
                            Id = 5,
                            Direccion = "Sede Barranquilla",
                            Eliminado = 0,
                            Estado = 1,
                            Nombre = "Proyecto Gas Natural Cuenca Magdalena"
                        });
                });

            modelBuilder.Entity("IntegradorNET.Entities.Servicio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("codServicio");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("VARCHAR(200)")
                        .HasColumnName("descr");

                    b.Property<int>("Eliminado")
                        .HasColumnType("int")
                        .HasColumnName("eliminado");

                    b.Property<int>("Estado")
                        .HasColumnType("int")
                        .HasColumnName("estado");

                    b.Property<decimal>("ValorHora")
                        .HasColumnType("DECIMAL(19,2)")
                        .HasColumnName("valorHora");

                    b.HasKey("Id");

                    b.ToTable("Servicios");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Descripcion = "Exploración Geofísica",
                            Eliminado = 0,
                            Estado = 1,
                            ValorHora = 500m
                        },
                        new
                        {
                            Id = 2,
                            Descripcion = "Peforación Direccional y Horizontal",
                            Eliminado = 0,
                            Estado = 1,
                            ValorHora = 1000m
                        },
                        new
                        {
                            Id = 3,
                            Descripcion = "Ingeniería de Reservorios",
                            Eliminado = 0,
                            Estado = 0,
                            ValorHora = 750m
                        },
                        new
                        {
                            Id = 4,
                            Descripcion = "Transporte y Logística",
                            Eliminado = 0,
                            Estado = 1,
                            ValorHora = 500m
                        },
                        new
                        {
                            Id = 5,
                            Descripcion = "Mantenimiento de Plataformas Offshore",
                            Eliminado = 0,
                            Estado = 1,
                            ValorHora = 1500m
                        });
                });

            modelBuilder.Entity("IntegradorNET.Entities.Trabajo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("codTrabajo");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CantHoras")
                        .HasColumnType("int")
                        .HasColumnName("cantHoras");

                    b.Property<decimal>("Costo")
                        .HasColumnType("DECIMAL(19,2)")
                        .HasColumnName("costo");

                    b.Property<int>("Eliminado")
                        .HasColumnType("int")
                        .HasColumnName("eliminado");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2")
                        .HasColumnName("fecha");

                    b.Property<int>("ProyectoId")
                        .HasColumnType("int")
                        .HasColumnName("codProyecto");

                    b.Property<int>("ServicioId")
                        .HasColumnType("int")
                        .HasColumnName("codServicio");

                    b.Property<decimal>("ValorHora")
                        .HasColumnType("DECIMAL(19,2)")
                        .HasColumnName("valorHora");

                    b.HasKey("Id");

                    b.HasIndex("ProyectoId");

                    b.HasIndex("ServicioId");

                    b.ToTable("Trabajos");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CantHoras = 35,
                            Costo = 10500m,
                            Eliminado = 0,
                            Fecha = new DateTime(2023, 9, 24, 18, 21, 25, 69, DateTimeKind.Local).AddTicks(1450),
                            ProyectoId = 1,
                            ServicioId = 2,
                            ValorHora = 300m
                        },
                        new
                        {
                            Id = 2,
                            CantHoras = 48,
                            Costo = 19200m,
                            Eliminado = 0,
                            Fecha = new DateTime(2023, 9, 24, 18, 21, 25, 69, DateTimeKind.Local).AddTicks(1480),
                            ProyectoId = 4,
                            ServicioId = 5,
                            ValorHora = 400m
                        },
                        new
                        {
                            Id = 3,
                            CantHoras = 35,
                            Costo = 11200m,
                            Eliminado = 0,
                            Fecha = new DateTime(2023, 9, 24, 18, 21, 25, 69, DateTimeKind.Local).AddTicks(1480),
                            ProyectoId = 4,
                            ServicioId = 2,
                            ValorHora = 320m
                        },
                        new
                        {
                            Id = 4,
                            CantHoras = 60,
                            Costo = 12000m,
                            Eliminado = 0,
                            Fecha = new DateTime(2023, 9, 24, 18, 21, 25, 69, DateTimeKind.Local).AddTicks(1480),
                            ProyectoId = 2,
                            ServicioId = 3,
                            ValorHora = 200m
                        },
                        new
                        {
                            Id = 5,
                            CantHoras = 35,
                            Costo = 15750m,
                            Eliminado = 0,
                            Fecha = new DateTime(2023, 9, 24, 18, 21, 25, 69, DateTimeKind.Local).AddTicks(1480),
                            ProyectoId = 1,
                            ServicioId = 1,
                            ValorHora = 450m
                        },
                        new
                        {
                            Id = 6,
                            CantHoras = 50,
                            Costo = 15000m,
                            Eliminado = 0,
                            Fecha = new DateTime(2023, 9, 24, 18, 21, 25, 69, DateTimeKind.Local).AddTicks(1480),
                            ProyectoId = 3,
                            ServicioId = 1,
                            ValorHora = 300m
                        },
                        new
                        {
                            Id = 7,
                            CantHoras = 45,
                            Costo = 18500m,
                            Eliminado = 0,
                            Fecha = new DateTime(2023, 9, 24, 18, 21, 25, 69, DateTimeKind.Local).AddTicks(1490),
                            ProyectoId = 5,
                            ServicioId = 4,
                            ValorHora = 400m
                        });
                });

            modelBuilder.Entity("IntegradorNET.Entities.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("codUsuario");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Contrasena")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("contrasena");

                    b.Property<int>("Dni")
                        .HasColumnType("int")
                        .HasColumnName("dni");

                    b.Property<int>("Eliminado")
                        .HasColumnType("int")
                        .HasColumnName("eliminado");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("email");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("VARCHAR(50)")
                        .HasColumnName("nombre");

                    b.Property<int>("Tipo")
                        .HasColumnType("int")
                        .HasColumnName("tipo");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Contrasena = "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4",
                            Dni = 26175345,
                            Eliminado = 0,
                            Email = "jperez@techoil.com",
                            Nombre = "Juan Pérez",
                            Tipo = 1
                        },
                        new
                        {
                            Id = 2,
                            Contrasena = "ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f",
                            Dni = 36565454,
                            Eliminado = 0,
                            Email = "jgomez@techoil.com",
                            Nombre = "José Gómez",
                            Tipo = 2
                        },
                        new
                        {
                            Id = 3,
                            Contrasena = "7d1a54127b222502f5b79b5fb0803061152a44f92b37e23c6527baf665d4da9a",
                            Dni = 32115082,
                            Eliminado = 0,
                            Email = "malvarez@techoil.com",
                            Nombre = "María Álvarez",
                            Tipo = 2
                        });
                });

            modelBuilder.Entity("IntegradorNET.Entities.Trabajo", b =>
                {
                    b.HasOne("IntegradorNET.Entities.Proyecto", "Proyecto")
                        .WithMany()
                        .HasForeignKey("ProyectoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IntegradorNET.Entities.Servicio", "Servicio")
                        .WithMany()
                        .HasForeignKey("ServicioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Proyecto");

                    b.Navigation("Servicio");
                });
#pragma warning restore 612, 618
        }
    }
}