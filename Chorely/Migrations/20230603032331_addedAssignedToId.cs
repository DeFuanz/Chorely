using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chorely.Migrations
{
    /// <inheritdoc />
    public partial class addedAssignedToId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AssignedToId",
                table: "Chore",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignedToId",
                table: "Chore");
        }
    }
}
