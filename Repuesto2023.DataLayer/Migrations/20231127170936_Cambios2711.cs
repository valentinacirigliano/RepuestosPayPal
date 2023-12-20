using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repuestos2023.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class Cambios2711 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RepuestosProveedores_Proveedores_ProveedorId",
                table: "RepuestosProveedores");

            migrationBuilder.DropForeignKey(
                name: "FK_RepuestosProveedores_Repuestos_RepuestoId",
                table: "RepuestosProveedores");

            migrationBuilder.DropIndex(
                name: "IX_RepuestosProveedores_ProveedorId",
                table: "RepuestosProveedores");

            migrationBuilder.DropIndex(
                name: "IX_RepuestosProveedores_RepuestoId",
                table: "RepuestosProveedores");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_RepuestosProveedores_ProveedorId",
                table: "RepuestosProveedores",
                column: "ProveedorId");

            migrationBuilder.CreateIndex(
                name: "IX_RepuestosProveedores_RepuestoId",
                table: "RepuestosProveedores",
                column: "RepuestoId");

            migrationBuilder.AddForeignKey(
                name: "FK_RepuestosProveedores_Proveedores_ProveedorId",
                table: "RepuestosProveedores",
                column: "ProveedorId",
                principalTable: "Proveedores",
                principalColumn: "ProveedorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RepuestosProveedores_Repuestos_RepuestoId",
                table: "RepuestosProveedores",
                column: "RepuestoId",
                principalTable: "Repuestos",
                principalColumn: "RepuestoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
