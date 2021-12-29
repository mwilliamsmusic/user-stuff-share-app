using Microsoft.EntityFrameworkCore.Migrations;

namespace user_stuff_share_app.Migrations
{
    public partial class meh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "flaggers",
                table: "flag");

            migrationBuilder.AddColumn<long>(
                name: "user_id",
                table: "flag",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "ix_flag_user_id",
                table: "flag",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_flag_user_user_id",
                table: "flag",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_flag_user_user_id",
                table: "flag");

            migrationBuilder.DropIndex(
                name: "ix_flag_user_id",
                table: "flag");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "flag");

            migrationBuilder.AddColumn<string>(
                name: "flaggers",
                table: "flag",
                type: "text",
                nullable: true);
        }
    }
}
