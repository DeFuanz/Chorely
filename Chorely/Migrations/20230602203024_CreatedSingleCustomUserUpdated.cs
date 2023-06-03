using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chorely.Migrations
{
    /// <inheritdoc />
    public partial class CreatedSingleCustomUserUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AssignedAdministrator",
                table: "AspNetUsers",
                newName: "AssignedAdministratorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AssignedAdministratorId",
                table: "AspNetUsers",
                newName: "AssignedAdministrator");
        }
    }
}
