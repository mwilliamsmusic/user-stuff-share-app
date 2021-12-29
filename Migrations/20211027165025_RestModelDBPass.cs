using Microsoft.EntityFrameworkCore.Migrations;

namespace user_stuff_share_app.Migrations
{
    public partial class RestModelDBPass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "passcode",
                table: "reset",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "passcode",
                table: "reset");
        }
    }
}
