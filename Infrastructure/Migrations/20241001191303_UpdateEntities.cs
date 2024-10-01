using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class UpdateEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Is_Maneger",
                table: "Employee");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId1",
                table: "Employee",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Maneger_Id",
                table: "Department",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Maneger_Name",
                table: "Department",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employee_DepartmentId1",
                table: "Employee",
                column: "DepartmentId1",
                unique: true,
                filter: "[DepartmentId1] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Department_DepartmentId1",
                table: "Employee",
                column: "DepartmentId1",
                principalTable: "Department",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Department_DepartmentId1",
                table: "Employee");

            migrationBuilder.DropIndex(
                name: "IX_Employee_DepartmentId1",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "DepartmentId1",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "Maneger_Id",
                table: "Department");

            migrationBuilder.DropColumn(
                name: "Maneger_Name",
                table: "Department");

            migrationBuilder.AddColumn<bool>(
                name: "Is_Maneger",
                table: "Employee",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
