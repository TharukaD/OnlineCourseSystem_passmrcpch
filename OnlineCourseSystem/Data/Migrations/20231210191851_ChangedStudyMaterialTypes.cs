using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineCourseSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangedStudyMaterialTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "StudyMaterialTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "StudyMaterialTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Pdf");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "StudyMaterialTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "PDF");

            migrationBuilder.InsertData(
                table: "StudyMaterialTypes",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "PPT" });
        }
    }
}
