using Microsoft.EntityFrameworkCore.Migrations;

namespace Dateingapp.API.Migrations
{
    public partial class sqlTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "likecount",
                table: "Likes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "likecount",
                table: "Likes");
        }
    }
}
