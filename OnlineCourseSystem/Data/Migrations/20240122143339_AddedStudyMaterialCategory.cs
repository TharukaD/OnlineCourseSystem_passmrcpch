using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineCourseSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedStudyMaterialCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudyMaterialCategoryId",
                table: "StudyMaterials",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StudyMaterialCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyMaterialCategories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudyMaterials_StudyMaterialCategoryId",
                table: "StudyMaterials",
                column: "StudyMaterialCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudyMaterials_StudyMaterialCategories_StudyMaterialCategoryId",
                table: "StudyMaterials",
                column: "StudyMaterialCategoryId",
                principalTable: "StudyMaterialCategories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudyMaterials_StudyMaterialCategories_StudyMaterialCategoryId",
                table: "StudyMaterials");

            migrationBuilder.DropTable(
                name: "StudyMaterialCategories");

            migrationBuilder.DropIndex(
                name: "IX_StudyMaterials_StudyMaterialCategoryId",
                table: "StudyMaterials");

            migrationBuilder.DropColumn(
                name: "StudyMaterialCategoryId",
                table: "StudyMaterials");
        }
    }
}
