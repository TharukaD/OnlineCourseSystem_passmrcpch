using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineCourseSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedStudyMaterialTopic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "StudyMaterialCategories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StudyMaterialTopicId",
                table: "StudyMaterialCategories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TopicId",
                table: "StudyMaterialCategories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StudyMaterialTopics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyMaterialTopics", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudyMaterialCategories_StudyMaterialTopicId",
                table: "StudyMaterialCategories",
                column: "StudyMaterialTopicId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyMaterialCategories_TopicId",
                table: "StudyMaterialCategories",
                column: "TopicId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudyMaterialCategories_StudyMaterialTopics_StudyMaterialTopicId",
                table: "StudyMaterialCategories",
                column: "StudyMaterialTopicId",
                principalTable: "StudyMaterialTopics",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudyMaterialCategories_StudyMaterialTopics_TopicId",
                table: "StudyMaterialCategories",
                column: "TopicId",
                principalTable: "StudyMaterialTopics",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudyMaterialCategories_StudyMaterialTopics_StudyMaterialTopicId",
                table: "StudyMaterialCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_StudyMaterialCategories_StudyMaterialTopics_TopicId",
                table: "StudyMaterialCategories");

            migrationBuilder.DropTable(
                name: "StudyMaterialTopics");

            migrationBuilder.DropIndex(
                name: "IX_StudyMaterialCategories_StudyMaterialTopicId",
                table: "StudyMaterialCategories");

            migrationBuilder.DropIndex(
                name: "IX_StudyMaterialCategories_TopicId",
                table: "StudyMaterialCategories");

            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "StudyMaterialCategories");

            migrationBuilder.DropColumn(
                name: "StudyMaterialTopicId",
                table: "StudyMaterialCategories");

            migrationBuilder.DropColumn(
                name: "TopicId",
                table: "StudyMaterialCategories");
        }
    }
}
