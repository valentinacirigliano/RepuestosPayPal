using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repuestos2023.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class CambiosShoppingCarts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_Repuestos_ProductId",
                table: "ShoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCarts_ProductId",
                table: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ShoppingCarts");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_RepuestoId",
                table: "ShoppingCarts",
                column: "RepuestoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_Repuestos_RepuestoId",
                table: "ShoppingCarts",
                column: "RepuestoId",
                principalTable: "Repuestos",
                principalColumn: "RepuestoId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_Repuestos_RepuestoId",
                table: "ShoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCarts_RepuestoId",
                table: "ShoppingCarts");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ShoppingCarts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_ProductId",
                table: "ShoppingCarts",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_Repuestos_ProductId",
                table: "ShoppingCarts",
                column: "ProductId",
                principalTable: "Repuestos",
                principalColumn: "RepuestoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
