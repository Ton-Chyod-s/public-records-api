using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiarioOficial.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class AttContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_AuthToken_AuthTokenÌd",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_AuthTokenÌd",
                table: "User");

            migrationBuilder.DropColumn(
                name: "AuthTokenÌd",
                table: "User");

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "AuthToken",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_AuthToken_UserId",
                table: "AuthToken",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthToken_User_UserId",
                table: "AuthToken",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthToken_User_UserId",
                table: "AuthToken");

            migrationBuilder.DropIndex(
                name: "IX_AuthToken_UserId",
                table: "AuthToken");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AuthToken");

            migrationBuilder.AddColumn<long>(
                name: "AuthTokenÌd",
                table: "User",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_User_AuthTokenÌd",
                table: "User",
                column: "AuthTokenÌd");

            migrationBuilder.AddForeignKey(
                name: "FK_User_AuthToken_AuthTokenÌd",
                table: "User",
                column: "AuthTokenÌd",
                principalTable: "AuthToken",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
