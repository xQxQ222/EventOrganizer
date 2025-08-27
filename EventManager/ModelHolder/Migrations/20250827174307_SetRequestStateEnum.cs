using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModelHolder.Migrations
{
    /// <inheritdoc />
    public partial class SetRequestStateEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "requests",
                type: "text",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "status",
                table: "requests",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
