using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repuestos2023.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class Cambios2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RepuestosProveedores",
                columns: table => new
                {
                    RepuestoProveedorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RepuestoId = table.Column<int>(type: "int", nullable: false),
                    ProveedorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepuestosProveedores", x => x.RepuestoProveedorId);
                    table.ForeignKey(
                        name: "FK_RepuestosProveedores_Proveedores_ProveedorId",
                        column: x => x.ProveedorId,
                        principalTable: "Proveedores",
                        principalColumn: "ProveedorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RepuestosProveedores_Repuestos_RepuestoId",
                        column: x => x.RepuestoId,
                        principalTable: "Repuestos",
                        principalColumn: "RepuestoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RepuestosProveedores_ProveedorId",
                table: "RepuestosProveedores",
                column: "ProveedorId");

            migrationBuilder.CreateIndex(
                name: "IX_RepuestosProveedores_RepuestoId",
                table: "RepuestosProveedores",
                column: "RepuestoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RepuestosProveedores");
        }
    }
}
