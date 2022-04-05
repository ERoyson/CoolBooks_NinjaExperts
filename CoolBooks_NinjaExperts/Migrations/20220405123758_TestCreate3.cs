using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoolBooks_NinjaExperts.Migrations
{
    public partial class TestCreate3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_AspNetUsers_UserIdId",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "UserIdId",
                table: "Books",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_UserIdId",
                table: "Books",
                newName: "IX_Books_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_AspNetUsers_UserId",
                table: "Books",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_AspNetUsers_UserId",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Books",
                newName: "UserIdId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_UserId",
                table: "Books",
                newName: "IX_Books_UserIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_AspNetUsers_UserIdId",
                table: "Books",
                column: "UserIdId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
