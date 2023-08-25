using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Anas_sBookShelf.WepApi.Migrations
{
    /// <inheritdoc />
    public partial class Done : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "UploaderImages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "CustomerImages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UploaderImages_BookId",
                table: "UploaderImages",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerImages_BookId",
                table: "CustomerImages",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerImages_CustomerId",
                table: "CustomerImages",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerImages_Books_BookId",
                table: "CustomerImages",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerImages_Customers_CustomerId",
                table: "CustomerImages",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UploaderImages_Books_BookId",
                table: "UploaderImages",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerImages_Books_BookId",
                table: "CustomerImages");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerImages_Customers_CustomerId",
                table: "CustomerImages");

            migrationBuilder.DropForeignKey(
                name: "FK_UploaderImages_Books_BookId",
                table: "UploaderImages");

            migrationBuilder.DropIndex(
                name: "IX_UploaderImages_BookId",
                table: "UploaderImages");

            migrationBuilder.DropIndex(
                name: "IX_CustomerImages_BookId",
                table: "CustomerImages");

            migrationBuilder.DropIndex(
                name: "IX_CustomerImages_CustomerId",
                table: "CustomerImages");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "UploaderImages");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "CustomerImages");
        }
    }
}
