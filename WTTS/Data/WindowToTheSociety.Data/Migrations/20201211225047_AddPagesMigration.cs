using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WindowToTheSociety.Data.Migrations
{
    public partial class AddPagesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PageId",
                table: "Posts",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Pages",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pages_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_PageId",
                table: "Posts",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_Pages_ApplicationUserId",
                table: "Pages",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Pages_IsDeleted",
                table: "Pages",
                column: "IsDeleted");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Pages_PageId",
                table: "Posts",
                column: "PageId",
                principalTable: "Pages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Pages_PageId",
                table: "Posts");

            migrationBuilder.DropTable(
                name: "Pages");

            migrationBuilder.DropIndex(
                name: "IX_Posts_PageId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "PageId",
                table: "Posts");
        }
    }
}
