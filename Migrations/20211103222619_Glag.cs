using System.Text.Json;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace user_stuff_share_app.Migrations
{
    public partial class Glag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "flagger");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "flag",
                type: "text",
                nullable: true,
                oldClrType: typeof(JsonDocument),
                oldType: "jsonb",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "flaggers",
                table: "flag",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "flaggers",
                table: "flag");

            migrationBuilder.AlterColumn<JsonDocument>(
                name: "status",
                table: "flag",
                type: "jsonb",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "flagger",
                columns: table => new
                {
                    flagger_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    flag_id = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_flagger", x => x.flagger_id);
                    table.ForeignKey(
                        name: "fk_flagger_flag_flag_id",
                        column: x => x.flag_id,
                        principalTable: "flag",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_flagger_flag_id",
                table: "flagger",
                column: "flag_id");
        }
    }
}
