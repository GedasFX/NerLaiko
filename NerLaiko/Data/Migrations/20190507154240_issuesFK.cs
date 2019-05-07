using Microsoft.EntityFrameworkCore.Migrations;

namespace NerLaiko.Data.Migrations
{
    public partial class issuesFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OperatorId",
                table: "Issues",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Issues_OperatorId",
                table: "Issues",
                column: "OperatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_AspNetUsers_OperatorId",
                table: "Issues",
                column: "OperatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_AspNetUsers_OperatorId",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_OperatorId",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "OperatorId",
                table: "Issues");
        }
    }
}
