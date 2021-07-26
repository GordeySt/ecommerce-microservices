using Microsoft.EntityFrameworkCore.Migrations;

namespace Identity.Infrastructure.Migrations
{
    public partial class RemoveSoftDeleteFromRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AspNetRoles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AspNetRoles",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
