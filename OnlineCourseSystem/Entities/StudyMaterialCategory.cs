using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OnlineCourseSystem.Entities
{
    public class StudyMaterialCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Priority { get; set; }
        public int? MyProperty { get; set; }
        public int? TopicId { get; set; }
        public virtual StudyMaterialTopic? Topic { get; set; }
    }

    public class StudyMaterialCategoryConfiguration : IEntityTypeConfiguration<StudyMaterialCategory>
    {
        public void Configure(EntityTypeBuilder<StudyMaterialCategory> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasOne(a => a.Topic)
              .WithMany()
              .HasForeignKey(a => a.TopicId);
        }
    }
}
