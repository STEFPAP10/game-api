using WebApplication1.Data;
using WebApplication1.Dtos;
using WebApplication1.Entities;
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
            group.MapPost("/",(CreateGameDto newGame, GameStoreContext dbContext) =>
            {
                    Game game = new()
                    {    Name = newGame.Name,
                         Genre = dbContext.Genres.Find(newGame.GenreId),
                         GenreId = newGame.GenreId,
                         Price = newGame.Price!.Value,
                         ReleaseDate = newGame.ReleaseDate!.Value
                    };
                    dbContext.Games.Add(game);
                    dbContext.SaveChanges();
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
                    updatedGame.Price!.Value,
                    updatedGame.ReleaseDate!.Value);
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