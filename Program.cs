using CatalogR.Data;
using CatalogR.Data.Migrations;
using CatalogR.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Syncfusion.Blazor;
using System.Data;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddServerSideBlazor();
builder.Services.AddSyncfusionBlazor();
builder.Services.AddControllersWithViews()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);

builder.Services.AddLocalization(options =>
{
    options.ResourcesPath = "Resources";
});

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("en-US"),
        new CultureInfo("ru-RU"),
    };

    options.DefaultRequestCulture = new RequestCulture("en-US");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ActiveUserPolicy", policyBuilder =>
    {
        policyBuilder.AddRequirements(new ActiveUserRequirement());
    });
});
builder.Services.AddTransient<IAuthorizationHandler, ActiveUserRequirementHandler>();

builder.Services.Configure<SecurityStampValidatorOptions>(options =>
{
    options.ValidationInterval = TimeSpan.Zero;
});

var app = builder.Build();
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NAaF1cXmhKYVVpR2Nbe050flREalxZVAciSV9jS3pTdEdrWXtfcnRdQGRVUA==");

app.UseRequestLocalization();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .RequireAuthorization("ActiveUserPolicy");
app.MapBlazorHub();
app.MapRazorPages()
   .RequireAuthorization("ActiveUserPolicy");

using (var scope = app.Services.CreateAsyncScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    var roles = new[] { "Admin" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

using (var scope = app.Services.CreateAsyncScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    string email = "admin@admin.com";
    string password = "Test1234,";

    if (await userManager.FindByEmailAsync(email) == null)
    {
        var user = new User();
        user.UserName = email;
        user.Email = email;

        await userManager.CreateAsync(user, password);
        await userManager.AddToRoleAsync(user, "Admin");
    }
}

using (var scope = app.Services.CreateAsyncScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var topics = new[] { "Books", "Signs", "Silverware" };
    foreach (var topic in topics)
    {
        if (!await db.CollectionTopics.AnyAsync(c => c.Name == topic))
        {
            await db.CollectionTopics.AddAsync(new CollectionTopic() { Name = topic });
        }
    }

    await db.SaveChangesAsync();
}

List<Tag> allTags = new List<Tag>();
using (var scope = app.Services.CreateAsyncScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var tags = new[] { "Cool", "Hot", "New", "Real" };
    foreach (var tag in tags)
    {
        Tag newTag = new Tag() { Name = tag };
        allTags.Add(newTag);
        if (!await db.Tags.AnyAsync(c => c.Name == tag))
        {
            await db.Tags.AddAsync(newTag);
        }
    }

    await db.SaveChangesAsync();
}

using (var scope = app.Services.CreateAsyncScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var items = new[] { "Item 1", "Book Hawking", "J.K. Rowling", "Stop Sign" };
    foreach (var item in items)
    {
        if (!await db.Items.AnyAsync(c => c.Name == item))
        {
            Item newItem = new Item() { Name = item };
            for (int i = 0; i < (new Random()).Next(0, allTags.Count); i++)
            {
                newItem.Tags.Add(allTags[i]);
            }

            await db.Items.AddAsync(newItem);
        }
    }

    await db.SaveChangesAsync();
}

app.Run();
