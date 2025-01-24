using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DiarioOficial.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTableAuthToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AuthTokenÌd",
                table: "User",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "AuthToken",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Bearer = table.Column<long>(type: "bigint", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthToken", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_AuthToken_AuthTokenÌd",
                table: "User");

            migrationBuilder.DropTable(
                name: "AuthToken");

            migrationBuilder.DropIndex(
                name: "IX_User_AuthTokenÌd",
                table: "User");

            migrationBuilder.DropColumn(
                name: "AuthTokenÌd",
                table: "User");
        }
    }
}
