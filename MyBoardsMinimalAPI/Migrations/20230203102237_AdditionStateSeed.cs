using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBoardsMinimalAPI.Migrations
{
    /// <inheritdoc />
    public partial class AdditionStateSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "States",
                column: "Name",
                value: "On Hold");

            migrationBuilder.InsertData(
                table: "States",
                column: "Name",
                value: "Rejected");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "Name",
                keyValue: "On Hold");

            migrationBuilder.DeleteData(
                table: "States",
                keyColumn: "Name",
                keyValue: "Rejected");
        }
    }
}
