using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace University_system.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] {Guid.NewGuid().ToString(),"Student","Student".ToUpper(),Guid.NewGuid().ToString()}
            );
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] {Guid.NewGuid().ToString(), "Finance Employee", "Finance Employee".ToUpper(),Guid.NewGuid().ToString()}
            );
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] {Guid.NewGuid().ToString(), "Professor", "Professor".ToUpper(),Guid.NewGuid().ToString()}
            );
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] {Guid.NewGuid().ToString(), "Affairs Employee", "Affairs Employee".ToUpper(),Guid.NewGuid().ToString()}
            );
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] {Guid.NewGuid().ToString(), "Dean", "Dean".ToUpper(),Guid.NewGuid().ToString()}
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [dbo].[Roles]");
        }
    }
}
