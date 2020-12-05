using Microsoft.EntityFrameworkCore.Migrations;

namespace WindowToTheSociety.Data.Migrations
{
    public partial class CreatePostEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Album_AspNetUsers_ApplicationUserId",
                table: "Album");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Album_PhotoId",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Album",
                table: "Album");

            migrationBuilder.RenameTable(
                name: "Album",
                newName: "Photo");

            migrationBuilder.RenameIndex(
                name: "IX_Album_ApplicationUserId",
                table: "Photo",
                newName: "IX_Photo_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Photo",
                table: "Photo",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Photo_AspNetUsers_ApplicationUserId",
                table: "Photo",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Photo_PhotoId",
                table: "Posts",
                column: "PhotoId",
                principalTable: "Photo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photo_AspNetUsers_ApplicationUserId",
                table: "Photo");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Photo_PhotoId",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Photo",
                table: "Photo");

            migrationBuilder.RenameTable(
                name: "Photo",
                newName: "Album");

            migrationBuilder.RenameIndex(
                name: "IX_Photo_ApplicationUserId",
                table: "Album",
                newName: "IX_Album_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Album",
                table: "Album",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Album_AspNetUsers_ApplicationUserId",
                table: "Album",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Album_PhotoId",
                table: "Posts",
                column: "PhotoId",
                principalTable: "Album",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
