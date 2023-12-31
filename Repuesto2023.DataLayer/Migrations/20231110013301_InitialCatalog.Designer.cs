﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repuestos2023.DataLayer.Data;

#nullable disable

namespace Repuestos2023.DataLayer.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231110013301_InitialCatalog")]
    partial class InitialCatalog
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Repuestos2023.Models.Models.Categoria", b =>
                {
                    b.Property<int>("CategoriaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoriaId"));

                    b.Property<string>("NombreCategoria")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("CategoriaId");

                    b.HasIndex("NombreCategoria")
                        .IsUnique();

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("Repuestos2023.Models.Models.Ciudad", b =>
                {
                    b.Property<int>("CiudadId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CiudadId"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProvinciaId")
                        .HasColumnType("int");

                    b.HasKey("CiudadId");

                    b.HasIndex("ProvinciaId");

                    b.ToTable("Ciudades");

                    b.HasData(
                        new
                        {
                            CiudadId = 1,
                            Nombre = "Lobos",
                            ProvinciaId = 1
                        });
                });

            modelBuilder.Entity("Repuestos2023.Models.Models.Proveedor", b =>
                {
                    b.Property<int>("ProveedorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProveedorId"));

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombreProveedor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProveedorId");

                    b.ToTable("Proveedores");
                });

            modelBuilder.Entity("Repuestos2023.Models.Models.Provincia", b =>
                {
                    b.Property<int>("ProvinciaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProvinciaId"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ProvinciaId");

                    b.ToTable("Provincias");

                    b.HasData(
                        new
                        {
                            ProvinciaId = 1,
                            Nombre = "Buenos Aires"
                        });
                });

            modelBuilder.Entity("Repuestos2023.Models.Models.Repuesto", b =>
                {
                    b.Property<int>("RepuestoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RepuestoId"));

                    b.Property<int>("CategoriaId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Imagen")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Precio100")
                        .HasColumnType("float");

                    b.Property<double>("Precio50")
                        .HasColumnType("float");

                    b.Property<double>("PrecioLista")
                        .HasColumnType("float");

                    b.Property<double>("PrecioUnitario")
                        .HasColumnType("float");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.HasKey("RepuestoId");

                    b.HasIndex("CategoriaId");

                    b.ToTable("Repuestos");
                });

            modelBuilder.Entity("Repuestos2023.Models.Models.Ciudad", b =>
                {
                    b.HasOne("Repuestos2023.Models.Models.Provincia", "Provincia")
                        .WithMany()
                        .HasForeignKey("ProvinciaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Provincia");
                });

            modelBuilder.Entity("Repuestos2023.Models.Models.Repuesto", b =>
                {
                    b.HasOne("Repuestos2023.Models.Models.Categoria", "Categoria")
                        .WithMany()
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categoria");
                });
#pragma warning restore 612, 618
        }
    }
}
