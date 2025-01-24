using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiarioOficial.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnAuthorized : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Authorized",
                table: "Person",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Authorized",
                table: "Person");
        }
    }
}
