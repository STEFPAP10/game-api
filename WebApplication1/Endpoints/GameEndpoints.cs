using WebApplication1.Dtos;
namespace WebApplication1.Endpoints;


public static class GameEndpoints 
{
    private static readonly List<GameDto> games = [
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

    public static RouteGroupBuilder MapGameEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/games")
                        .WithParameterValidation();
            // GET /games        
           group.MapGet("/",()=>games);        

           // GET /games/{id}
           group.MapGet("/{id}", (int id) =>
            {
                    var game = games.FirstOrDefault(g => g.Id == id);
                    if (game is null)
                    {
                         return Results.NotFound();
                    }
                        return Results.Ok(game);
            }).WithName("GetName"); 

            // POST /games
            group.MapPost("/",(CreateGameDto newGame) =>
            {
                 
                GameDto game = new(
                    games.Count > 0 ? games.Max(g => g.Id) + 1 : 1,
                    newGame.Name,
                    newGame.Genre,
                    newGame.Price!.Value,
                    newGame.ReleaseDate!.Value);
                    games.Add(game);
                    return Results.CreatedAtRoute("GetName", new { id = game.Id }, game);
            });
            
            // PUT /games/
            group.MapPut("/{id}",(int id, UpdateGameDto updatedGame) =>
            {
               var index = games.FindIndex(game => game.Id == id);
               if (index == -1)
               {
                    return Results.NotFound();
               }
               GameDto game = new(
                    id,
                    updatedGame.Name,
                    updatedGame.Genre,
                    updatedGame.Price,
                    updatedGame.ReleaseDate);
                games[index] = game;
                return Results.Ok(game);
            });

            // DELETE /games/{id}
            group.MapDelete("/{id}", (int id) =>
            {
                var index = games.FindIndex(game => game.Id == id);
                if (index == -1)
                {
                    return Results.NotFound();
                }
                games.RemoveAt(index);
                return Results.NoContent();
            });
        return group;
    }

}