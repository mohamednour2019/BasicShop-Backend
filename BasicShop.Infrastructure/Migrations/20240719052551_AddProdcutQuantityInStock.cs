using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasicShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProdcutQuantityInStock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuantityInStock",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuantityInStock",
                table: "Product");
        }
    }
}
