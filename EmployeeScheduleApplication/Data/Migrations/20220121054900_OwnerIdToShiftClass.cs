using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeScheduleApplication.Data.Migrations
{
    public partial class OwnerIdToShiftClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Shift",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Shift");
        }
    }
}
