using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NerLaiko.Data.Migrations
{
    public partial class loyaltylevel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LoyaltyLevelId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LoyaltyLevels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Years = table.Column<int>(nullable: false),
                    Coefficient = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoyaltyLevels", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_LoyaltyLevelId",
                table: "AspNetUsers",
                column: "LoyaltyLevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_LoyaltyLevels_LoyaltyLevelId",
                table: "AspNetUsers",
                column: "LoyaltyLevelId",
                principalTable: "LoyaltyLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_LoyaltyLevels_LoyaltyLevelId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "LoyaltyLevels");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_LoyaltyLevelId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LoyaltyLevelId",
                table: "AspNetUsers");
        }
    }
}
