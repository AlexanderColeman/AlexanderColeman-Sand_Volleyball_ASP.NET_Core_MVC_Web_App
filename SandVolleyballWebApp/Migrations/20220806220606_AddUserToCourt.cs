using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SandVolleyballWebApp.Migrations
{
    public partial class AddUserToCourt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Courts_myCourtsId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_myCourtsId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "myCourtsId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Courts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courts_AppUserId",
                table: "Courts",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courts_AspNetUsers_AppUserId",
                table: "Courts",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courts_AspNetUsers_AppUserId",
                table: "Courts");

            migrationBuilder.DropIndex(
                name: "IX_Courts_AppUserId",
                table: "Courts");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Courts");

            migrationBuilder.AddColumn<int>(
                name: "myCourtsId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_myCourtsId",
                table: "AspNetUsers",
                column: "myCourtsId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Courts_myCourtsId",
                table: "AspNetUsers",
                column: "myCourtsId",
                principalTable: "Courts",
                principalColumn: "Id");
        }
    }
}
