using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Anas_sBookShelf.WepApi.Migrations
{
    /// <inheritdoc />
    public partial class cart_status : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Carts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Carts");
        }
    }
}
