using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OnlineCourseSystem.Entities
{
    public class StudyMaterialTopic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Priority { get; set; }
        public virtual IList<StudyMaterialCategory> StudyMaterialCategories { get; set; }
    }

    public class StudyMaterialTopicConfiguration : IEntityTypeConfiguration<StudyMaterialTopic>
    {
        public void Configure(EntityTypeBuilder<StudyMaterialTopic> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);
        }
    }
}
