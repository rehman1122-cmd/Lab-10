using Microsoft.EntityFrameworkCore;
using PMS.Data;
using PMS.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
var connectionString = builder.Configuration.GetConnectionString("PmsDatabase")
    ?? throw new InvalidOperationException("The PmsDatabase connection string is missing.");
builder.Services.AddDbContext<PmsDbContext>(options =>
    options.UseSqlite(connectionString));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllers();

await using (var scope = app.Services.CreateAsyncScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PmsDbContext>();
    await db.Database.EnsureCreatedAsync();

    if (!await db.Watches.AnyAsync())
    {
        db.Watches.AddRange(
            new Watch
            {
                Brand = "Rolex",
                Model = "Datejust",
                Price = 8500.00m,
                StockQuantity = 4,
                Description = "Classic automatic watch with a silver dial."
            },
            new Watch
            {
                Brand = "Omega",
                Model = "Seamaster",
                Price = 6200.00m,
                StockQuantity = 3,
                Description = "Sport watch with a blue dial."
            });

        await db.SaveChangesAsync();
    }
}

app.Run();
