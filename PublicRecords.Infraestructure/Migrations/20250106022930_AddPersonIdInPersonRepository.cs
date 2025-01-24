using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiarioOficial.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPersonIdInPersonRepository : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OfficialStateDiaries_Person_PersonId",
                table: "OfficialStateDiaries");

            migrationBuilder.DropForeignKey(
                name: "FK_OfficialStateDiaries_Sessions_SessionId",
                table: "OfficialStateDiaries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OfficialStateDiaries",
                table: "OfficialStateDiaries");

            migrationBuilder.RenameTable(
                name: "OfficialStateDiaries",
                newName: "OfficialDiaries");

            migrationBuilder.RenameIndex(
                name: "IX_OfficialStateDiaries_SessionId",
                table: "OfficialDiaries",
                newName: "IX_OfficialDiaries_SessionId");

            migrationBuilder.RenameIndex(
                name: "IX_OfficialStateDiaries_PersonId",
                table: "OfficialDiaries",
                newName: "IX_OfficialDiaries_PersonId");

            migrationBuilder.AlterColumn<long>(
                name: "SessionId",
                table: "OfficialDiaries",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OfficialDiaries",
                table: "OfficialDiaries",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OfficialDiaries_Person_PersonId",
                table: "OfficialDiaries",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OfficialDiaries_Sessions_SessionId",
                table: "OfficialDiaries",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OfficialDiaries_Person_PersonId",
                table: "OfficialDiaries");

            migrationBuilder.DropForeignKey(
                name: "FK_OfficialDiaries_Sessions_SessionId",
                table: "OfficialDiaries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OfficialDiaries",
                table: "OfficialDiaries");

            migrationBuilder.RenameTable(
                name: "OfficialDiaries",
                newName: "OfficialStateDiaries");

            migrationBuilder.RenameIndex(
                name: "IX_OfficialDiaries_SessionId",
                table: "OfficialStateDiaries",
                newName: "IX_OfficialStateDiaries_SessionId");

            migrationBuilder.RenameIndex(
                name: "IX_OfficialDiaries_PersonId",
                table: "OfficialStateDiaries",
                newName: "IX_OfficialStateDiaries_PersonId");

            migrationBuilder.AlterColumn<long>(
                name: "SessionId",
                table: "OfficialStateDiaries",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OfficialStateDiaries",
                table: "OfficialStateDiaries",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OfficialStateDiaries_Person_PersonId",
                table: "OfficialStateDiaries",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OfficialStateDiaries_Sessions_SessionId",
                table: "OfficialStateDiaries",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
