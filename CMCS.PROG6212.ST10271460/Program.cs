using CMCS.PROG6212.ST10271460.Models;
using CMCS.PROG6212.ST10271460.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddSignalR(); // Add SignalR for real-time updates
builder.Services.AddScoped<IUserService, UserService>(); // Register the IUserService interface with UserService implementation
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite("Data Source=localDatabase.db"));


var app = builder.Build();


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.MapHub<ClaimHub>("/claimHub");

app.Run();

