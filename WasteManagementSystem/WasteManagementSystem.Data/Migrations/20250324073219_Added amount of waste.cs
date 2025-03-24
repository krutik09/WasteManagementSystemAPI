using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WasteManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class Addedamountofwaste : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WasteAmount",
                table: "Orders",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WasteAmount",
                table: "Orders");
        }
    }
}
