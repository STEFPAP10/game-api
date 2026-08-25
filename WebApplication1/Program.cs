using WebApplication1.Dtos;


var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

List<GameDto> games = [
    new(
        1,
        "Fifa 23",
        "Sports",
        59.99M,
        new DateOnly(2022, 9, 30)),
    new(
        2,
        "Street Fighter II",
        "Fighting",
        19.99M,
        new DateOnly(1991, 3, 1)),
    new(
        3,
        "Final Fantasy VII",
        "Role-Playing",
        29.99M,
        new DateOnly(1997, 1, 31))
];

// GET /games
app.MapGet("/games",()=>games);

// GET /games/{id}
app.MapGet("/games/{id}", (int id) =>
{
    var game = games.FirstOrDefault(g => g.Id == id);
    if (game is null)
    {
        return Results.NotFound();
    }
    return Results.Ok(game);
}).WithName("GetName");

// POST /games
app.MapPost("/games",(CreateGameDto newGame) =>
{
    GameDto game = new(
        games.Count +1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate);
        games.Add(game);
        return Results.CreatedAtRoute("GetName", new { id = game.Id }, game);
});

// PUT /games/
app.MapPut("/games/{id}",(int id, UpdateGameDto updatedGame) =>
{
   var index = games.FindIndex(game => game.Id == id);

   if (index == -1)
    {
        return Results.NotFound();
    }

    games[index] = new GameDto(
        id,
        updatedGame.Name,
        updatedGame.Genre,
        updatedGame.Price,
        updatedGame.ReleaseDate);

        return Results.NoContent();


});


app.Run();
