using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repuestos2023.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class NuevosCambiosEnUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "State",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "ZipCode",
                table: "AspNetUsers",
                newName: "Direccion");

            migrationBuilder.RenameColumn(
                name: "StreetAddress",
                table: "AspNetUsers",
                newName: "CodPostal");

            migrationBuilder.AddColumn<int>(
                name: "CiudadId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProvinciaId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CiudadId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProvinciaId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Direccion",
                table: "AspNetUsers",
                newName: "ZipCode");

            migrationBuilder.RenameColumn(
                name: "CodPostal",
                table: "AspNetUsers",
                newName: "StreetAddress");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
