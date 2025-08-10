namespace GameStore.Api.Dtos;

public record class GameDto(
    int Id,
    decimal Price,
    string Name,
    string Genre,
    DateOnly REleaseDate);

