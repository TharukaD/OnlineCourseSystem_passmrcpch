using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineCourseSystem;
using OnlineCourseSystem.Data;
using OnlineCourseSystem.Middleware;
using OnlineCourseSystem.Services.Article;
using OnlineCourseSystem.Services.ArticleCategory;
using OnlineCourseSystem.Services.CounterRecord;
using OnlineCourseSystem.Services.Course;
using OnlineCourseSystem.Services.EmailService;
using OnlineCourseSystem.Services.HomePageBanner;
using OnlineCourseSystem.Services.Inquiry;
using OnlineCourseSystem.Services.Serivice;
using OnlineCourseSystem.Services.Tag;
using VitalCareWeb.Extensions;
using static OnlineCourseSystem.Constants.ModelConstants;

var builder = WebApplication.CreateBuilder(args);

//-- configure email
builder.Services.ConfigureEmailService(builder.Configuration);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true)
    .AddRazorRuntimeCompilation();

builder.Services.AddTransient<ExceptionHandlingMiddleware>();

//----- Services Manage
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<IArticleCategoryService, ArticleCategoryService>();
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<IServiceService, ServiceService>();
builder.Services.AddScoped<IHomePageBannerService, HomePageBannerService>();
builder.Services.AddScoped<ICounterRecordService, CounterRecordService>();
builder.Services.AddScoped<IInquiryService, InquiryService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IEmailService, EmailService>();

//---- Mapper Configuration
IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    var roles = new[] { Roles.Admin, Roles.Teacher, Roles.Student };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    string email = "admin@gmail.com";
    string password = "Admin@123";
    if (await userManager.FindByEmailAsync(email) == null)
    {
        var user = new IdentityUser();
        user.UserName = email;
        user.Email = email;
        user.EmailConfirmed = true;
        await userManager.CreateAsync(user, password);

        await userManager.AddToRoleAsync(user, Roles.Admin);
    }
}

app.Run();
