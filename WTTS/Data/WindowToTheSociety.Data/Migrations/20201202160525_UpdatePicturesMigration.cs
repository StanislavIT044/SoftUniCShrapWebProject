using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WindowToTheSociety.Data.Migrations
{
    public partial class UpdatePicturesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "ProfilePictures",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Photos",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "CoverPhotos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "ProfilePictures");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "CoverPhotos");
        }
    }
}
