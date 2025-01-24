using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiarioOficial.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLongToStringUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Bearer",
                table: "AuthToken",
                type: "text",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Bearer",
                table: "AuthToken",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
