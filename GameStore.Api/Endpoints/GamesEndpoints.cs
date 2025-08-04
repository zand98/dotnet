using GameStore.Api.Dtos;
using Microsoft.VisualBasic;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{


    const string GetGameEndpointName = "GetGame";
    private static readonly List<GameDto> games = [
        new(1, "Street Fighter I", "Fighting", 19.99M, new DateOnly(1992, 7, 15)),
        new(2, "Final Fantasy", "RolePlaying", 59.99M, new DateOnly(2010, 9, 30)),
    ];

    public static WebApplication MapGamesEndpoints(this WebApplication app)
    {

        var group = app.MapGroup("games").WithParameterValidation();

        group.MapGet("/", () => games);

        group.MapGet("/{id}", (int id) =>
        {
            GameDto? game = games.Find(game => game.Id == id);

            return game is null ? Results.NotFound() : Results.Ok(game);

        }).WithName(GetGameEndpointName);

        group.MapPost("/", (CreateGameDto createGameDto) =>
        {
            Console.WriteLine("createGameDto: " + createGameDto);
            GameDto game = new(
            games.Count + 1,
            createGameDto.Name,
            createGameDto.Genre,
            createGameDto.Price,
            createGameDto.ReleaseDate);

            games.Add(game);

            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
        });

        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) =>
        {
            var index = games.FindIndex(game => game.Id == id);

            if (index == -1)
            {
                return Results.NotFound();
            }

            Console.WriteLine("updatedGame: " + updatedGame);
            Console.WriteLine("index: " + index);
            Console.WriteLine("games: " + games);

            games[index] = new GameDto(
                id,
                updatedGame.Name,
                updatedGame.Genre,
                updatedGame.Price,
                updatedGame.ReleaseDate

            );
            Console.WriteLine("games after update: " + games);
            return Results.NoContent();
        });

        group.MapDelete("/{id}", (int id) =>
        {
            var index = games.FindIndex(game => game.Id == id);
            if (index < 0)
            {
                return Results.NotFound();
            }

            games.RemoveAt(index);
            return Results.NoContent();
        });

        return app;
    }

}