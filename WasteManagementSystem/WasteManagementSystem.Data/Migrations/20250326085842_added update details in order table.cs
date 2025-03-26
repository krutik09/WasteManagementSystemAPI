using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WasteManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedupdatedetailsinordertable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LastUpdatedByUserId",
                table: "Orders",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedDate",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastUpdatedByUserId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "LastUpdatedDate",
                table: "Orders");
        }
    }
}
