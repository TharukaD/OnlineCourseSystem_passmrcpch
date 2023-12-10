using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineCourseSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedURLToStudyMaterial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VideoUrl",
                table: "StudyMaterials");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "StudyMaterials",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "StudyMaterials");

            migrationBuilder.AddColumn<string>(
                name: "VideoUrl",
                table: "StudyMaterials",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
