using Microsoft.EntityFrameworkCore.Migrations;

namespace user_stuff_share_app.Migrations
{
    public partial class Flagging : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "url",
                table: "flag",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "url",
                table: "flag");
        }
    }
}
