using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiarioOficial.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class AlterConfigTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Person_NameID",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "User",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "User",
                newName: "PassWordHash");

            migrationBuilder.RenameColumn(
                name: "NameID",
                table: "Sessions",
                newName: "PersonID");

            migrationBuilder.RenameIndex(
                name: "IX_Sessions_NameID",
                table: "Sessions",
                newName: "IX_Sessions_PersonID");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "User",
                type: "boolean",
                nullable: true,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: true);

            migrationBuilder.AddColumn<long>(
                name: "PersonId",
                table: "OfficialStateDiaries",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_OfficialStateDiaries_PersonId",
                table: "OfficialStateDiaries",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_OfficialStateDiaries_Person_PersonId",
                table: "OfficialStateDiaries",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Person_PersonID",
                table: "Sessions",
                column: "PersonID",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OfficialStateDiaries_Person_PersonId",
                table: "OfficialStateDiaries");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Person_PersonID",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_OfficialStateDiaries_PersonId",
                table: "OfficialStateDiaries");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "OfficialStateDiaries");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "User",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "PassWordHash",
                table: "User",
                newName: "PasswordHash");

            migrationBuilder.RenameColumn(
                name: "PersonID",
                table: "Sessions",
                newName: "NameID");

            migrationBuilder.RenameIndex(
                name: "IX_Sessions_PersonID",
                table: "Sessions",
                newName: "IX_Sessions_NameID");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "User",
                type: "boolean",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true,
                oldDefaultValue: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "User",
                type: "text",
                nullable: false,
                defaultValue: "seuemail@seuemail.com");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Person_NameID",
                table: "Sessions",
                column: "NameID",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
