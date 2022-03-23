using FishesBackend.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Для БД в оперативной памяти
//builder.Services.AddDbContext<BreedDb>(options => options.UseInMemoryDatabase("items"));
builder.Services.AddDbContext<FishDb>(options => options.UseInMemoryDatabase("items"));

// Для подключения к SQL Server
// string connectionString = "Server=localhost,1433;Database=FishesDB;User Id=sa;Password=<YourStrong@Passw0rd>;Integrated Security=True;";
// builder.Services.AddSqlite<BreedDb>(connectionString);
// builder.Services.AddSqlite<FishDb>(connectionString);

// Для подключения к SQLite
// var connectionString = builder.Configuration.GetConnectionString("Breeds") ?? "Data Source=Fishes.db";

// builder.Services.AddSqlServer<BreedDb>(connectionString);
// builder.Services.AddSqlServer<FishDb>(connectionString);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.AllowAnyOrigin();
                    });
});


var app = builder.Build();

app.MapGet("/breeds", async (FishDb db) => await db.Breeds.ToListAsync());
app.MapPost("/breeds", async (FishDb db, Breed breed) =>
{
    await db.Breeds.AddAsync(breed);
    await db.SaveChangesAsync();
    return Results.Created($"/breed/{breed.id}", breed);
});

app.MapPut("/breeds/{id}", async (FishDb db, Breed update, int id) =>
{
    var breed = await db.Breeds.FindAsync(id);
    if (breed is null) return Results.NotFound();
    breed.name = update.name;
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/breeds/{id}", async (FishDb db, int id) =>
{
    var breed = await db.Breeds.FindAsync(id);
    if (breed is null)
    {
        return Results.NotFound();
    }
    db.Breeds.Remove(breed);
    await db.SaveChangesAsync();
    return Results.Ok();
});


app.MapGet("/fishes", async (FishDb db) => await db.Fishes.ToListAsync());
app.MapPost("/fishes", async (FishDb db, Fish fish) =>
{
    await db.Fishes.AddAsync(fish);
    await db.SaveChangesAsync();
    return Results.Created($"/fishes/{fish.id}", fish);
});

app.MapPut("/fishes/{id}", async (FishDb db, Fish update, int id) =>
{
    var fish = await db.Fishes.FindAsync(id);
    if (fish is null) return Results.NotFound();
    fish.name = update.name;
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/fishes/{id}", async (FishDb db, int id) =>
{
    var fish = await db.Fishes.FindAsync(id);
    if (fish is null)
    {
        return Results.NotFound();
    }
    db.Fishes.Remove(fish);
    await db.SaveChangesAsync();
    return Results.Ok();
});



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseCors(builder => builder.AllowAnyOrigin());

app.Run();

