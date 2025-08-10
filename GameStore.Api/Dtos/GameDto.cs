namespace GameStore.Api.Dtos;

public record class GameDto(
    int Id,
    string Name,
    decimal Price,
    string Genre,
    DateOnly REleaseDate);

