using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace OnlineCourseSystem.Data;

using OnlineCourseSystem.Entities;
public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Tag> Tags { get; set; }
    public DbSet<Article> Articles { get; set; }
    public DbSet<ArticleTag> ArticleTags { get; set; }
    public DbSet<ArticleCategory> ArticleCategories { get; set; }

    public DbSet<Service> Services { get; set; }
    public DbSet<CounterRecord> CounterRecords { get; set; }
    public DbSet<HomePageBanner> HomePageBanners { get; set; }
    public DbSet<Inquiry> Inquiries { get; set; }
    public DbSet<Course> Courses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new TagConfiguration());
        modelBuilder.ApplyConfiguration(new ArticleConfiguration());
        modelBuilder.ApplyConfiguration(new ArticleTagConfiguration());
        modelBuilder.ApplyConfiguration(new ArticleCategoryConfiguration());
        modelBuilder.ApplyConfiguration(new CourseConfiguration());
        modelBuilder.ApplyConfiguration(new ServiceConfiguration());
        modelBuilder.ApplyConfiguration(new CounterRecordConfiguration());
        modelBuilder.ApplyConfiguration(new HomePageBannerConfiguration());
        modelBuilder.ApplyConfiguration(new InquiryConfiguration());
    }
}
