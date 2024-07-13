using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Meguri.Migrations
{
    /// <inheritdoc />
    public partial class AddUserName2Works : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Works",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Works");
        }
    }
}
