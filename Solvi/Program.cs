using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Solvi.Data;
using Solvi.Middleware;
using Solvi.Models;
using Solvi.ServiceInterfaces;
using Solvi.Services;
using System.Net.Http.Headers;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews().AddNewtonsoftJson(options => { options.UseMemberCasing(); });
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<UserProfilePictureActionFilter>();
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();

builder.Services.Configure<GeneralConfig>(builder.Configuration.GetSection("General"));

builder.Services.AddTransient<IGeneralService, GeneralService>();
builder.Services.AddScoped<IEmailService, EmailService>();

// Add localization services
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

// Register the IStringLocalizerFactory service
builder.Services.AddSingleton<IStringLocalizerFactory, ResourceManagerStringLocalizerFactory>();

builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
builder.Services.Configure<RouteOptions>(options =>
{
    options.ConstraintMap["hyphenated"] = typeof(HyphenatedRouteConstraint);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseMigrationsEndPoint();
    app.UseDeveloperExceptionPage();
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
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
//app.MapHub<NotificationHub>("/notificationHub");

app.UseMiddleware<UserContextMiddleware>();

app.Use(async (context, next) =>
{
    string? cookie = "";
    if (context.Request.Cookies.TryGetValue("Language", out cookie))
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cookie);
        System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(cookie);
    }
    else
    {
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en");
        System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
    }
    await next.Invoke();
});

app.Run();
