using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Meguri.Migrations
{
    /// <inheritdoc />
    public partial class PluralTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Work_AspNetUsers_UserId",
                table: "Work");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Work",
                table: "Work");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UploadFile",
                table: "UploadFile");

            migrationBuilder.RenameTable(
                name: "Work",
                newName: "Works");

            migrationBuilder.RenameTable(
                name: "UploadFile",
                newName: "UploadFiles");

            migrationBuilder.RenameIndex(
                name: "IX_Work_UserId",
                table: "Works",
                newName: "IX_Works_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Work_Updated",
                table: "Works",
                newName: "IX_Works_Updated");

            migrationBuilder.RenameIndex(
                name: "IX_Work_Tag3",
                table: "Works",
                newName: "IX_Works_Tag3");

            migrationBuilder.RenameIndex(
                name: "IX_Work_Tag2",
                table: "Works",
                newName: "IX_Works_Tag2");

            migrationBuilder.RenameIndex(
                name: "IX_Work_Tag1",
                table: "Works",
                newName: "IX_Works_Tag1");

            migrationBuilder.RenameIndex(
                name: "IX_Work_ParentId",
                table: "Works",
                newName: "IX_Works_ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_Work_Created",
                table: "Works",
                newName: "IX_Works_Created");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Works",
                table: "Works",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UploadFiles",
                table: "UploadFiles",
                column: "Id");

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

            migrationBuilder.DropPrimaryKey(
                name: "PK_Works",
                table: "Works");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UploadFiles",
                table: "UploadFiles");

            migrationBuilder.RenameTable(
                name: "Works",
                newName: "Work");

            migrationBuilder.RenameTable(
                name: "UploadFiles",
                newName: "UploadFile");

            migrationBuilder.RenameIndex(
                name: "IX_Works_UserId",
                table: "Work",
                newName: "IX_Work_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Works_Updated",
                table: "Work",
                newName: "IX_Work_Updated");

            migrationBuilder.RenameIndex(
                name: "IX_Works_Tag3",
                table: "Work",
                newName: "IX_Work_Tag3");

            migrationBuilder.RenameIndex(
                name: "IX_Works_Tag2",
                table: "Work",
                newName: "IX_Work_Tag2");

            migrationBuilder.RenameIndex(
                name: "IX_Works_Tag1",
                table: "Work",
                newName: "IX_Work_Tag1");

            migrationBuilder.RenameIndex(
                name: "IX_Works_ParentId",
                table: "Work",
                newName: "IX_Work_ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_Works_Created",
                table: "Work",
                newName: "IX_Work_Created");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Work",
                table: "Work",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UploadFile",
                table: "UploadFile",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Work_AspNetUsers_UserId",
                table: "Work",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
