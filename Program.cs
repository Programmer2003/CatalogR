using CatalogR.Hubs;
using CatalogR.Data;
using CatalogR.Models;
using CatalogR.Services;
using CatalogR.CloudStorage;
using CatalogR.Data.Migrations;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Authorization;
using Syncfusion.Blazor;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString, opts =>
        opts.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
    ));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<User>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 5;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddServerSideBlazor();
builder.Services.Configure<HubOptions>(options =>
{
    options.MaximumReceiveMessageSize = 1024 * 1024; // 1MB or use null
});
builder.Services.AddSyncfusionBlazor();
builder.Services.AddControllersWithViews()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();

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
    options.AddPolicy("AdminPolicy", policyBuilder =>
    {
        policyBuilder.AddRequirements(new AdminRequirement());
    });
    options.AddPolicy("ActiveUserPolicy", policyBuilder =>
    {
        policyBuilder.AddRequirements(new ActiveUserRequirement());
    });
    options.AddPolicy("CollectionAccessPolicy", policyBuilder =>
    {
        policyBuilder.AddRequirements(new CollectionAccessRequirement());
    });
    options.AddPolicy("UserCollectionPolicy", policyBuilder =>
    {
        policyBuilder.AddRequirements(new UserCollectionRequirement());
    });
});
builder.Services.AddTransient<IAuthorizationHandler, AdminRequirementHandler>();
builder.Services.AddTransient<IAuthorizationHandler, ActiveUserRequirementHandler>();
builder.Services.AddTransient<IAuthorizationHandler, CollectionAccessRequirementHandler>();
builder.Services.AddTransient<IAuthorizationHandler, UserCollectionRequirementHandler>();

builder.Services.Configure<SecurityStampValidatorOptions>(options =>
{
    options.ValidationInterval = TimeSpan.Zero;
});

builder.Services.AddSingleton<ICloudStorage, GoogleCloudStorage>();
builder.Services.AddTransient<FullTextSearchService>();
builder.Services.AddTransient<HtmlSanitizationService>();
builder.Services.AddTransient<TimeManagerService>();
builder.Services.AddTransient<RazorLocalizerService>();
builder.Services.AddTransient<TopicsLocalizerService>();
builder.Services.AddTransient<AdminService>();

var app = builder.Build();
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(builder.Configuration.GetValue<string>("SyncfusionLicenseKey"));

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

app.UseStatusCodePagesWithRedirects("/error/{0}");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<CommentsHub>("/commentshub");
    endpoints.MapHub<LikesHub>("/likeshub");
});

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
        user.IsAdmin = true;

        await userManager.CreateAsync(user, password);
        await userManager.AddToRoleAsync(user, "Admin");
    }
}

using (var scope = app.Services.CreateAsyncScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var topics = new[] { "Books", "Cars", "History", "Fashion", "Sport", "Cuisine", "Myth", "Technology", "Science", "Other" };
    foreach (var topic in topics)
    {
        if (!await db.CollectionTopics.AnyAsync(c => c.Name == topic))
        {
            await db.CollectionTopics.AddAsync(new CollectionTopic() { Name = topic });
        }
    }

    await db.SaveChangesAsync();
}

using (var scope = app.Services.CreateAsyncScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var tags = new[] { "Cool", "Hot", "New", "Real" };
    foreach (var tag in tags)
    {
        if (!await db.Tags.AnyAsync(c => c.Name == tag))
        {
            await db.Tags.AddAsync(new Tag() { Name = tag });
        }
    }

    await db.SaveChangesAsync();
}

app.Run();
