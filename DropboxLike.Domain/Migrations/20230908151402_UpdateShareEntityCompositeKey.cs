using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DropboxLike.Domain.Migrations
{
    /// <inheritdoc />
    public partial class UpdateShareEntityCompositeKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SharedFiles",
                table: "SharedFiles");

            migrationBuilder.DropColumn(
                name: "RecipientEmail",
                table: "SharedFiles");

            migrationBuilder.DropColumn(
                name: "SenderEmail",
                table: "SharedFiles");

            migrationBuilder.AlterColumn<string>(
                name: "FileId",
                table: "SharedFiles",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "ShareId",
                table: "SharedFiles",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SharedFiles",
                table: "SharedFiles",
                column: "ShareId");

            migrationBuilder.CreateIndex(
                name: "IX_SharedFiles_UserId_FileId",
                table: "SharedFiles",
                columns: new[] { "UserId", "FileId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SharedFiles_AppUsers_UserId",
                table: "SharedFiles",
                column: "UserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SharedFiles_AppUsers_UserId",
                table: "SharedFiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SharedFiles",
                table: "SharedFiles");

            migrationBuilder.DropIndex(
                name: "IX_SharedFiles_UserId_FileId",
                table: "SharedFiles");

            migrationBuilder.DropColumn(
                name: "ShareId",
                table: "SharedFiles");

            migrationBuilder.AlterColumn<string>(
                name: "FileId",
                table: "SharedFiles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "RecipientEmail",
                table: "SharedFiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SenderEmail",
                table: "SharedFiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SharedFiles",
                table: "SharedFiles",
                column: "UserId");
        }
    }
}
