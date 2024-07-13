using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Meguri.Migrations
{
    /// <inheritdoc />
    public partial class ChageUserID2UserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Works_AspNetUsers_UserID",
                table: "Works");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Works",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Works_UserID",
                table: "Works",
                newName: "IX_Works_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Works_AspNetUsers_UserId",
                table: "Works",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Works_AspNetUsers_UserId",
                table: "Works");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Works",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Works_UserId",
                table: "Works",
                newName: "IX_Works_UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Works_AspNetUsers_UserID",
                table: "Works",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
