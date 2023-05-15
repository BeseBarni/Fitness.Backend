using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fitness.Backend.Domain.Migrations
{
    /// <inheritdoc />
    public partial class SportImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageId",
                table: "Sports",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sports_ImageId",
                table: "Sports",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sports_ProfilePictures_ImageId",
                table: "Sports",
                column: "ImageId",
                principalTable: "ProfilePictures",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sports_ProfilePictures_ImageId",
                table: "Sports");

            migrationBuilder.DropIndex(
                name: "IX_Sports_ImageId",
                table: "Sports");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Sports");
        }
    }
}
