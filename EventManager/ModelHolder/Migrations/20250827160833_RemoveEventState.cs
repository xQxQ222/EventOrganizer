using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModelHolder.Migrations
{
    /// <inheritdoc />
    public partial class RemoveEventState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "state",
                table: "events");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "state",
                table: "events",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);
        }
    }
}
