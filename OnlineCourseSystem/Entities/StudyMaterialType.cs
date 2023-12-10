using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static OnlineCourseSystem.Constants.ModelConstants;

namespace OnlineCourseSystem.Entities
{
    public class StudyMaterialType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class StudyMaterialTypeConfiguration : IEntityTypeConfiguration<StudyMaterialType>
    {
        public void Configure(EntityTypeBuilder<StudyMaterialType> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(200);

            //---- Seeding Material Types
            builder.HasData(
                new StudyMaterialType
                {
                    Id = 1,
                    Name = FileTypes.Video,
                },
                new StudyMaterialType
                {
                    Id = 2,
                    Name = FileTypes.Pdf,
                }
            );
        }
    }
}
