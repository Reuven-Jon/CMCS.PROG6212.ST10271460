using Microsoft.EntityFrameworkCore;
using CMCS.PROG6212.ST10271460.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// If you're in development and want to use the in-memory database:
if (builder.Environment.IsDevelopment())
{
    // Use an In-Memory database for development instead of SQL Server
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseInMemoryDatabase("CMCSDatabase"));  // Using In-Memory database
}
else
{
    // Use SQL Server when not in development
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
}

// Add controllers with views
builder.Services.AddControllersWithViews();

// Enable session management
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);  // Set session timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Authentication service
builder.Services.AddAuthentication("CookieAuth").AddCookie("CookieAuth", config =>
{
    config.Cookie.Name = "UserLoginCookie";          // Name of the auth cookie
    config.LoginPath = "/Account/Login";             // Redirect to login page if unauthorized
});

// SignalR setup
builder.Services.AddSignalR();

var app = builder.Build();

// Configure middleware based on environment
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();  // Shows detailed exception page
}
else
{
    app.UseExceptionHandler("/Home/Error");  // Custom error page for production
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();          // Enable session handling
app.UseAuthentication();   // Enable authentication
app.UseAuthorization();    // Enable authorization

// Map SignalR hub for real-time communication
app.MapHub<ClaimHub>("/claimHub");

// Change default route to point to Welcome page
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Welcome}/{id?}");

app.Run();
