using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace user_stuff_share_app.Migrations
{
    public partial class gah : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tag",
                columns: table => new
                {
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tag", x => x.name);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    username = table.Column<string>(type: "text", nullable: true),
                    password = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<string>(type: "text", nullable: true),
                    joined = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "collect",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    title = table.Column<string>(type: "text", nullable: true),
                    collect_form = table.Column<string>(type: "text", nullable: true),
                    image_path = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<string>(type: "text", nullable: true),
                    created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_collect", x => x.id);
                    table.ForeignKey(
                        name: "fk_collect_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "follow_user",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    follow_user_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_follow_user", x => x.id);
                    table.ForeignKey(
                        name: "fk_follow_user_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "item",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    collect_id = table.Column<long>(type: "bigint", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    title = table.Column<string>(type: "text", nullable: true),
                    item_form = table.Column<string>(type: "text", nullable: true),
                    image_path = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<string>(type: "text", nullable: true),
                    created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_item", x => x.id);
                    table.ForeignKey(
                        name: "fk_item_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "bookmark_collect",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    collect_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_bookmark_collect", x => x.id);
                    table.ForeignKey(
                        name: "fk_bookmark_collect_collect_collect_id",
                        column: x => x.collect_id,
                        principalTable: "collect",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_bookmark_collect_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cool_collect",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    collect_id = table.Column<long>(type: "bigint", nullable: false),
                    value = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cool_collect", x => x.id);
                    table.ForeignKey(
                        name: "fk_cool_collect_collect_collect_id",
                        column: x => x.collect_id,
                        principalTable: "collect",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tag_collect_join",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    collect_id = table.Column<long>(type: "bigint", nullable: false),
                    tag_name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tag_collect_join", x => x.id);
                    table.ForeignKey(
                        name: "fk_tag_collect_join_collect_collect_id",
                        column: x => x.collect_id,
                        principalTable: "collect",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "bookmark_item",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    item_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_bookmark_item", x => x.id);
                    table.ForeignKey(
                        name: "fk_bookmark_item_item_item_id",
                        column: x => x.item_id,
                        principalTable: "item",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_bookmark_item_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cool_item",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    item_id = table.Column<long>(type: "bigint", nullable: false),
                    value = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cool_item", x => x.id);
                    table.ForeignKey(
                        name: "fk_cool_item_item_item_id",
                        column: x => x.item_id,
                        principalTable: "item",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tag_item_join",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    item_id = table.Column<long>(type: "bigint", nullable: false),
                    tag_name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tag_item_join", x => x.id);
                    table.ForeignKey(
                        name: "fk_tag_item_join_item_item_id",
                        column: x => x.item_id,
                        principalTable: "item",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_tag_item_join_tag_tag_name",
                        column: x => x.tag_name,
                        principalTable: "tag",
                        principalColumn: "name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "cool_collect_join",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cool_collect_id = table.Column<long>(type: "bigint", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cool_collect_join", x => x.id);
                    table.ForeignKey(
                        name: "fk_cool_collect_join_cool_collect_cool_collect_id",
                        column: x => x.cool_collect_id,
                        principalTable: "cool_collect",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_cool_collect_join_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cool_item_join",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cool_item_id = table.Column<long>(type: "bigint", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cool_item_join", x => x.id);
                    table.ForeignKey(
                        name: "fk_cool_item_join_cool_item_cool_item_id",
                        column: x => x.cool_item_id,
                        principalTable: "cool_item",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_cool_item_join_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_bookmark_collect_collect_id",
                table: "bookmark_collect",
                column: "collect_id");

            migrationBuilder.CreateIndex(
                name: "ix_bookmark_collect_user_id",
                table: "bookmark_collect",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_bookmark_item_item_id",
                table: "bookmark_item",
                column: "item_id");

            migrationBuilder.CreateIndex(
                name: "ix_bookmark_item_user_id",
                table: "bookmark_item",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_collect_user_id",
                table: "collect",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_cool_collect_collect_id",
                table: "cool_collect",
                column: "collect_id");

            migrationBuilder.CreateIndex(
                name: "ix_cool_collect_join_cool_collect_id",
                table: "cool_collect_join",
                column: "cool_collect_id");

            migrationBuilder.CreateIndex(
                name: "ix_cool_collect_join_user_id",
                table: "cool_collect_join",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_cool_item_item_id",
                table: "cool_item",
                column: "item_id");

            migrationBuilder.CreateIndex(
                name: "ix_cool_item_join_cool_item_id",
                table: "cool_item_join",
                column: "cool_item_id");

            migrationBuilder.CreateIndex(
                name: "ix_cool_item_join_user_id",
                table: "cool_item_join",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_follow_user_user_id",
                table: "follow_user",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_item_user_id",
                table: "item",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_tag_collect_join_collect_id",
                table: "tag_collect_join",
                column: "collect_id");

            migrationBuilder.CreateIndex(
                name: "ix_tag_item_join_item_id",
                table: "tag_item_join",
                column: "item_id");

            migrationBuilder.CreateIndex(
                name: "ix_tag_item_join_tag_name",
                table: "tag_item_join",
                column: "tag_name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bookmark_collect");

            migrationBuilder.DropTable(
                name: "bookmark_item");

            migrationBuilder.DropTable(
                name: "cool_collect_join");

            migrationBuilder.DropTable(
                name: "cool_item_join");

            migrationBuilder.DropTable(
                name: "follow_user");

            migrationBuilder.DropTable(
                name: "tag_collect_join");

            migrationBuilder.DropTable(
                name: "tag_item_join");

            migrationBuilder.DropTable(
                name: "cool_collect");

            migrationBuilder.DropTable(
                name: "cool_item");

            migrationBuilder.DropTable(
                name: "tag");

            migrationBuilder.DropTable(
                name: "collect");

            migrationBuilder.DropTable(
                name: "item");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
