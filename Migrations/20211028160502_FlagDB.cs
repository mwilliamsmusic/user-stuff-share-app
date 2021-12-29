using System.Text.Json;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace user_stuff_share_app.Migrations
{
    public partial class FlagDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "flag",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    reason = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    rating = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_flag", x => x.id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "flagger");

            migrationBuilder.DropTable(
                name: "flag");
        }
    }
}
