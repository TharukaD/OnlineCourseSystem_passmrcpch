using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineCourseSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedPriorityToCourses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Priority",
                table: "Courses");
        }
    }
}
