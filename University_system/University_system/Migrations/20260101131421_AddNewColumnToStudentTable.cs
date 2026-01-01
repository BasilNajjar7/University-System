using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace University_system.Migrations
{
    /// <inheritdoc />
    public partial class AddNewColumnToStudentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CostOfHour",
                table: "AspNetUsers",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CostOfHour",
                table: "AspNetUsers");
        }
    }
}
