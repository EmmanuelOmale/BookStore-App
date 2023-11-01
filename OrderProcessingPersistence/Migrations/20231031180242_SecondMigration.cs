using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderProcessingPersistence.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookId",
                table: "Carts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BookId",
                table: "Carts",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
