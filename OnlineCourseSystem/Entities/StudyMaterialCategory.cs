using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OnlineCourseSystem.Entities
{
    public class StudyMaterialCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Priority { get; set; }
    }

    public class StudyMaterialCategoryConfiguration : IEntityTypeConfiguration<StudyMaterialCategory>
    {
        public void Configure(EntityTypeBuilder<StudyMaterialCategory> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);
        }

    }
}
