using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ModelHolder.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    category_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    category_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("categories_pkey", x => x.category_id);
                });

            migrationBuilder.CreateTable(
                name: "locations",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    latitude = table.Column<decimal>(type: "numeric(9,6)", precision: 9, scale: 6, nullable: false),
                    longitude = table.Column<decimal>(type: "numeric(9,6)", precision: 9, scale: 6, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("locations_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    role_id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    role_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("roles_pkey", x => x.role_id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    telegram_id = table.Column<long>(type: "bigint", nullable: false),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    role_id = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("users_pkey", x => x.telegram_id);
                    table.ForeignKey(
                        name: "users_role_id_fkey",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "role_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "events",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    title = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    location_id = table.Column<long>(type: "bigint", nullable: false),
                    created_on = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    event_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    is_paid = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false),
                    participant_limit = table.Column<int>(type: "integer", nullable: false, defaultValue: 10),
                    category_id = table.Column<long>(type: "bigint", nullable: false),
                    state = table.Column<short>(type: "smallint", nullable: false),
                    initiator_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("events_pkey", x => x.id);
                    table.ForeignKey(
                        name: "events_category_id_fkey",
                        column: x => x.category_id,
                        principalTable: "categories",
                        principalColumn: "category_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "events_initiator_id_fkey",
                        column: x => x.initiator_id,
                        principalTable: "users",
                        principalColumn: "telegram_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "events_location_id_fkey",
                        column: x => x.location_id,
                        principalTable: "locations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "comments",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    author_id = table.Column<long>(type: "bigint", nullable: false),
                    event_id = table.Column<long>(type: "bigint", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    is_positive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(700)", maxLength: 700, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("comments_pkey", x => x.id);
                    table.ForeignKey(
                        name: "comments_author_id_fkey",
                        column: x => x.author_id,
                        principalTable: "users",
                        principalColumn: "telegram_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "comments_event_id_fkey",
                        column: x => x.event_id,
                        principalTable: "events",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "images",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    event_id = table.Column<long>(type: "bigint", nullable: false),
                    path = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("images_pkey", x => x.id);
                    table.ForeignKey(
                        name: "images_event_id_fkey",
                        column: x => x.event_id,
                        principalTable: "events",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "requests",
                columns: table => new
                {
                    request_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    requester_id = table.Column<long>(type: "bigint", nullable: false),
                    event_id = table.Column<long>(type: "bigint", nullable: false),
                    status = table.Column<short>(type: "smallint", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("requests_pkey", x => x.request_id);
                    table.ForeignKey(
                        name: "requests_event_id_fkey",
                        column: x => x.event_id,
                        principalTable: "events",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "requests_requester_id_fkey",
                        column: x => x.requester_id,
                        principalTable: "users",
                        principalColumn: "telegram_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "categories_category_name_key",
                table: "categories",
                column: "category_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_comments_event_id",
                table: "comments",
                column: "event_id");

            migrationBuilder.CreateIndex(
                name: "uq_comment",
                table: "comments",
                columns: new[] { "author_id", "event_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_events_category_id",
                table: "events",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_events_initiator_id",
                table: "events",
                column: "initiator_id");

            migrationBuilder.CreateIndex(
                name: "IX_events_location_id",
                table: "events",
                column: "location_id");

            migrationBuilder.CreateIndex(
                name: "IX_images_event_id",
                table: "images",
                column: "event_id");

            migrationBuilder.CreateIndex(
                name: "uq_location_coordinates",
                table: "locations",
                columns: new[] { "latitude", "longitude" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_requests_event_id",
                table: "requests",
                column: "event_id");

            migrationBuilder.CreateIndex(
                name: "uq_event_requestor",
                table: "requests",
                columns: new[] { "requester_id", "event_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "roles_role_name_key",
                table: "roles",
                column: "role_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_role_id",
                table: "users",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "users_email_key",
                table: "users",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "comments");

            migrationBuilder.DropTable(
                name: "images");

            migrationBuilder.DropTable(
                name: "requests");

            migrationBuilder.DropTable(
                name: "events");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "locations");

            migrationBuilder.DropTable(
                name: "roles");
        }
    }
}
