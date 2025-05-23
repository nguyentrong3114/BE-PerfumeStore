using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BE_AMPerfume.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentDetail1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentItems_Payments_PaymentId",
                table: "PaymentItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentItems_ProductVariants_ProductVariantId",
                table: "PaymentItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentItems",
                table: "PaymentItems");

            migrationBuilder.RenameTable(
                name: "PaymentItems",
                newName: "PaymentDetails");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentItems_ProductVariantId",
                table: "PaymentDetails",
                newName: "IX_PaymentDetails_ProductVariantId");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentItems_PaymentId",
                table: "PaymentDetails",
                newName: "IX_PaymentDetails_PaymentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentDetails",
                table: "PaymentDetails",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentDetails_Payments_PaymentId",
                table: "PaymentDetails",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentDetails_ProductVariants_ProductVariantId",
                table: "PaymentDetails",
                column: "ProductVariantId",
                principalTable: "ProductVariants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentDetails_Payments_PaymentId",
                table: "PaymentDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentDetails_ProductVariants_ProductVariantId",
                table: "PaymentDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentDetails",
                table: "PaymentDetails");

            migrationBuilder.RenameTable(
                name: "PaymentDetails",
                newName: "PaymentItems");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentDetails_ProductVariantId",
                table: "PaymentItems",
                newName: "IX_PaymentItems_ProductVariantId");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentDetails_PaymentId",
                table: "PaymentItems",
                newName: "IX_PaymentItems_PaymentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentItems",
                table: "PaymentItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentItems_Payments_PaymentId",
                table: "PaymentItems",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentItems_ProductVariants_ProductVariantId",
                table: "PaymentItems",
                column: "ProductVariantId",
                principalTable: "ProductVariants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
