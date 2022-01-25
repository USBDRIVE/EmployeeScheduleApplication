using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeScheduleApplication.Data.Migrations
{
    public partial class ScheduleClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ScheduleId",
                table: "Shift",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Schedule",
                columns: table => new
                {
                    ScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScheduleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedule", x => x.ScheduleId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shift_ScheduleId",
                table: "Shift",
                column: "ScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shift_Schedule_ScheduleId",
                table: "Shift",
                column: "ScheduleId",
                principalTable: "Schedule",
                principalColumn: "ScheduleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shift_Schedule_ScheduleId",
                table: "Shift");

            migrationBuilder.DropTable(
                name: "Schedule");

            migrationBuilder.DropIndex(
                name: "IX_Shift_ScheduleId",
                table: "Shift");

            migrationBuilder.DropColumn(
                name: "ScheduleId",
                table: "Shift");
        }
    }
}
