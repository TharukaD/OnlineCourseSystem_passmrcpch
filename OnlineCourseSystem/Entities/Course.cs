using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OnlineCourseSystem.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string? Image { get; set; }
        public int Price { get; set; }
        public int OldPrice { get; set; }
        public bool IsPublished { get; set; }
        public int Priority { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
    }

    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.ShortDescription)
               .IsRequired()
               .HasMaxLength(400);

            builder.Property(x => x.LongDescription)
                .IsRequired();

            builder.Property(x => x.Image)
                .HasMaxLength(200);

            builder.Property(x => x.CreatedBy)
              .IsRequired()
              .HasMaxLength(200);
        }

    }
}
