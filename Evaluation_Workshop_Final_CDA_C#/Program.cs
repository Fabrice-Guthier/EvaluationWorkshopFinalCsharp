using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Evaluation_Workshop_Final_CDA_C_.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Evaluation_Workshop_Final_CDA_C_Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Evaluation_Workshop_Final_CDA_C_Context") ?? throw new InvalidOperationException("Connection string 'Evaluation_Workshop_Final_CDA_C_Context' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        // On appelle notre méthode Initialize
        DataSeeder.Initialize(services);
    }
    catch (Exception ex)
    {
        // En cas d'erreur pendant le seeding, on la logue
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
