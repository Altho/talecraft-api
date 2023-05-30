using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaleCraft.Migrations
{
    /// <inheritdoc />
    public partial class removedSaleParam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PasswordSalt",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
