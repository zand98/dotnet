using GameStore.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

List<GameDto> games = [
 new(1, "Street Fighter I", "Fighting", 19.99M, new DateOnly (1992, 7, 15)),
 new(2, "Final Fantasy", "RolePlaying", 59.99M, new DateOnly (2010, 9, 30)),
];

app.MapGet("games", () => games);

// app.MapGet("games/{id}",(int id)=> )

app.MapGet("/", () => "Hello World!");

app.Run();
