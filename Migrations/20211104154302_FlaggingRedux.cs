using Microsoft.EntityFrameworkCore.Migrations;

namespace user_stuff_share_app.Migrations
{
    public partial class FlaggingRedux : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "flag");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "flag",
                type: "text",
                nullable: true);
        }
    }
}
