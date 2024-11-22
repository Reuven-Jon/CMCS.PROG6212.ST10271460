
using CMCS.PROG6212.ST10271460.Data;
using CMCS.PROG6212.ST10271460.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// SignalR for real-time updates
builder.Services.AddSignalR();

// Add Authentication and Authorization
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AcademicManagerPolicy", policy => policy.RequireRole("AcademicManager"));
    options.AddPolicy("LecturerPolicy", policy => policy.RequireRole("Lecturer"));
    options.AddPolicy("HRPolicy", policy => policy.RequireRole("HR"));
});

// Configure Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    // Optional: Configure password policies
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Configure Database Context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    // For development/testing: In-memory database
    options.UseInMemoryDatabase("CMCSDatabase");

    // For production: Use a relational database like SQL Server (example below)
    // Uncomment this line and configure your connection string in appsettings.json
    // options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Dependency Injection for custom services
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Middleware pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); // Ensure session middleware is added before authentication

app.UseAuthentication();
app.UseAuthorization();

// Route configuration
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.MapControllerRoute(
    name: "lecturer",
    pattern: "Lecturer/{action=Dashboard}/{id?}",
    defaults: new { controller = "Lecturer", action = "Dashboard" });

app.MapControllerRoute(
    name: "manager",
    pattern: "Manager/{action=Dashboard}/{id?}",
    defaults: new { controller = "Manager", action = "Dashboard" });


app.MapHub<ClaimHub>("/claimHub");

// Seed the database with roles, users, and test data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        await SeedData.Initialize(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the database.");
    }
}

app.Run();


