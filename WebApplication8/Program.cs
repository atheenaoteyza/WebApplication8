using WebApplication8.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication8.Service;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container
builder.Services.AddControllersWithViews();

// Register DbContext
builder.Services.AddDbContext<BookstoreContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BookstoreContext")));

// Register your custom service
builder.Services.AddScoped<BookstoreService>();

var app = builder.Build();

// ✅ Global Error Handler (replace built-in exception pages)
app.UseMiddleware<ErrorHandlingMiddleware>();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    // ❌ Remove this since we're using our own middleware
    // app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
