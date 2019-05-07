using Microsoft.EntityFrameworkCore.Migrations;

namespace NerLaiko.Data.Migrations
{
    public partial class RefrigiratoruserIdstring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ItemDiscounts_DiscountId",
                table: "ItemDiscounts");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Refrigerators",
                nullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_ItemDiscounts_DiscountId_ItemId",
                table: "ItemDiscounts",
                columns: new[] { "DiscountId", "ItemId" });

            migrationBuilder.CreateIndex(
                name: "IX_Refrigerators_UserId",
                table: "Refrigerators",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Refrigerators_AspNetUsers_UserId",
                table: "Refrigerators",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Refrigerators_AspNetUsers_UserId",
                table: "Refrigerators");

            migrationBuilder.DropIndex(
                name: "IX_Refrigerators_UserId",
                table: "Refrigerators");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_ItemDiscounts_DiscountId_ItemId",
                table: "ItemDiscounts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Refrigerators");

            migrationBuilder.CreateIndex(
                name: "IX_ItemDiscounts_DiscountId",
                table: "ItemDiscounts",
                column: "DiscountId");
        }
    }
}
