using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WasteManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class changingusertypename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserTypeName",
                table: "UserTypes",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "UserTypes",
                newName: "UserTypeName");
        }
    }
}
