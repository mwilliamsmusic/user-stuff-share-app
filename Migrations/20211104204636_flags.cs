using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace user_stuff_share_app.Migrations
{
    public partial class flags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "flag");

            migrationBuilder.CreateTable(
                name: "collect_flag",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    reason = table.Column<string>(type: "text", nullable: true),
                    rating = table.Column<string>(type: "text", nullable: true),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    url = table.Column<string>(type: "text", nullable: true),
                    collect_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_collect_flag", x => x.id);
                    table.ForeignKey(
                        name: "fk_collect_flag_collect_collect_id",
                        column: x => x.collect_id,
                        principalTable: "collect",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_collect_flag_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "item_flag",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    reason = table.Column<string>(type: "text", nullable: true),
                    rating = table.Column<string>(type: "text", nullable: true),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    url = table.Column<string>(type: "text", nullable: true),
                    item_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_item_flag", x => x.id);
                    table.ForeignKey(
                        name: "fk_item_flag_item_item_id",
                        column: x => x.item_id,
                        principalTable: "item",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_item_flag_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_collect_flag_collect_id",
                table: "collect_flag",
                column: "collect_id");

            migrationBuilder.CreateIndex(
                name: "ix_collect_flag_user_id",
                table: "collect_flag",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_item_flag_item_id",
                table: "item_flag",
                column: "item_id");

            migrationBuilder.CreateIndex(
                name: "ix_item_flag_user_id",
                table: "item_flag",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "collect_flag");

            migrationBuilder.DropTable(
                name: "item_flag");

            migrationBuilder.CreateTable(
                name: "flag",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    rating = table.Column<string>(type: "text", nullable: true),
                    reason = table.Column<string>(type: "text", nullable: true),
                    url = table.Column<string>(type: "text", nullable: true),
                    user_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_flag", x => x.id);
                    table.ForeignKey(
                        name: "fk_flag_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_flag_user_id",
                table: "flag",
                column: "user_id");
        }
    }
}
