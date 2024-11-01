using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_biblioteca.Migrations
{
    /// <inheritdoc />
    public partial class PhotoPublicationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhotosPublication_Photos_IdPhoto",
                table: "PhotosPublication");

            migrationBuilder.DropForeignKey(
                name: "FK_PhotosPublication_Publications_IdPublication",
                table: "PhotosPublication");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PhotosPublication",
                table: "PhotosPublication");

            migrationBuilder.RenameTable(
                name: "PhotosPublication",
                newName: "PhotoPublication");

            migrationBuilder.RenameIndex(
                name: "IX_PhotosPublication_IdPhoto",
                table: "PhotoPublication",
                newName: "IX_PhotoPublication_IdPhoto");

            migrationBuilder.AddColumn<int>(
                name: "PhotoId",
                table: "PhotoPublication",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PublicationId",
                table: "PhotoPublication",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhotoPublication",
                table: "PhotoPublication",
                columns: new[] { "IdPublication", "IdPhoto" });

            migrationBuilder.CreateIndex(
                name: "IX_PhotoPublication_PhotoId",
                table: "PhotoPublication",
                column: "PhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoPublication_PublicationId",
                table: "PhotoPublication",
                column: "PublicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoPublication_Photos_IdPhoto",
                table: "PhotoPublication",
                column: "IdPhoto",
                principalTable: "Photos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoPublication_Photos_PhotoId",
                table: "PhotoPublication",
                column: "PhotoId",
                principalTable: "Photos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoPublication_Publications_IdPublication",
                table: "PhotoPublication",
                column: "IdPublication",
                principalTable: "Publications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoPublication_Publications_PublicationId",
                table: "PhotoPublication",
                column: "PublicationId",
                principalTable: "Publications",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhotoPublication_Photos_IdPhoto",
                table: "PhotoPublication");

            migrationBuilder.DropForeignKey(
                name: "FK_PhotoPublication_Photos_PhotoId",
                table: "PhotoPublication");

            migrationBuilder.DropForeignKey(
                name: "FK_PhotoPublication_Publications_IdPublication",
                table: "PhotoPublication");

            migrationBuilder.DropForeignKey(
                name: "FK_PhotoPublication_Publications_PublicationId",
                table: "PhotoPublication");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PhotoPublication",
                table: "PhotoPublication");

            migrationBuilder.DropIndex(
                name: "IX_PhotoPublication_PhotoId",
                table: "PhotoPublication");

            migrationBuilder.DropIndex(
                name: "IX_PhotoPublication_PublicationId",
                table: "PhotoPublication");

            migrationBuilder.DropColumn(
                name: "PhotoId",
                table: "PhotoPublication");

            migrationBuilder.DropColumn(
                name: "PublicationId",
                table: "PhotoPublication");

            migrationBuilder.RenameTable(
                name: "PhotoPublication",
                newName: "PhotosPublication");

            migrationBuilder.RenameIndex(
                name: "IX_PhotoPublication_IdPhoto",
                table: "PhotosPublication",
                newName: "IX_PhotosPublication_IdPhoto");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhotosPublication",
                table: "PhotosPublication",
                columns: new[] { "IdPublication", "IdPhoto" });

            migrationBuilder.AddForeignKey(
                name: "FK_PhotosPublication_Photos_IdPhoto",
                table: "PhotosPublication",
                column: "IdPhoto",
                principalTable: "Photos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PhotosPublication_Publications_IdPublication",
                table: "PhotosPublication",
                column: "IdPublication",
                principalTable: "Publications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
