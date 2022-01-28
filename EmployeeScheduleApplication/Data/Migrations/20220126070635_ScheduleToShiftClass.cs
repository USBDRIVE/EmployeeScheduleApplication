using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeScheduleApplication.Data.Migrations
{
    public partial class ScheduleToShiftClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shift_Schedule_ScheduleId",
                table: "Shift");

            migrationBuilder.AlterColumn<Guid>(
                name: "ScheduleId",
                table: "Shift",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Shift_Schedule_ScheduleId",
                table: "Shift",
                column: "ScheduleId",
                principalTable: "Schedule",
                principalColumn: "ScheduleId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shift_Schedule_ScheduleId",
                table: "Shift");

            migrationBuilder.AlterColumn<Guid>(
                name: "ScheduleId",
                table: "Shift",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Shift_Schedule_ScheduleId",
                table: "Shift",
                column: "ScheduleId",
                principalTable: "Schedule",
                principalColumn: "ScheduleId");
        }
    }
}
