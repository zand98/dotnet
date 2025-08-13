using GameStore.Api.Data;
using GameStore.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("GameStore");

builder.Services.AddSqlite<GameStoreContext>(connString);
// builder.Services.AddScoped<GameStoreContext>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
    var genres = context.Genres.ToList();

    Console.WriteLine("Seeded Genres:");
    foreach (var genre in genres) Console.WriteLine($"{genre.Id}: {genre.Name}");
}


app.MapGamesEndpoints();

app.MigrateDb();

app.Run();