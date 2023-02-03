using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBoardsMinimalAPI.Migrations
{
    /// <inheritdoc />
    public partial class AdditionTagSeed1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Tags",
                column: "Value",
                value: "API");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Value",
                keyValue: "API");
        }
    }
}
