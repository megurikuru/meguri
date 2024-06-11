using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace meguri.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexToCreatedColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Work_Created",
                table: "Work",
                column: "Created");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Work_Created",
                table: "Work");
        }
    }
}
