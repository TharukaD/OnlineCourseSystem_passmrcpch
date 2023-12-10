using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OnlineCourseSystem.Entities
{
    public class StudyMaterial
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? ShortDescription { get; set; }
        public string? LongDescription { get; set; }
        public string? Image { get; set; }
        public string? FileName { get; set; }
        public int StudyMaterialTypeId { get; set; }
        public StudyMaterialType StudyMaterialType { get; set; }
        public bool IsPublished { get; set; }
        public int Priority { get; set; }
    }

    public class StudyMaterialConfiguration : IEntityTypeConfiguration<StudyMaterial>
    {
        public void Configure(EntityTypeBuilder<StudyMaterial> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(a => a.FileName)
                .HasMaxLength(200);

            builder.HasOne(a => a.StudyMaterialType)
               .WithMany()
               .HasForeignKey(a => a.StudyMaterialTypeId);
        }
    }
}
