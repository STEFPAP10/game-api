using WebApplication1.Data;
using WebApplication1.Dtos;
using WebApplication1.Entities;
using WebApplication1.Mapping;
namespace WebApplication1.Endpoints;


public static class GameEndpoints 
{
    private static readonly List<GameSummaryDto> games = [
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
           group.MapGet("/{id}", (int id, GameStoreContext dbContext) =>
            {
                   Game? game = dbContext.Games.Find(id);

                   return game is not null ? Results.Ok(game.ToGameDetailsDto()) : Results.NotFound();
            }).WithName("GetName"); 

            // POST /games
            group.MapPost("/",(CreateGameDto newGame, GameStoreContext dbContext) =>
            {

                    Game  game = newGame.ToEntity();
                    

                    
                    dbContext.Games.Add(game);
                    dbContext.SaveChanges();
 

                    return Results.CreatedAtRoute("GetName", new { id = game.Id },game.ToGameDetailsDto());
            });
            
            // PUT /games/
            group.MapPut("/{id}",(int id, UpdateGameDto updatedGame) =>
            {
               var index = games.FindIndex(game => game.Id == id);
               if (index == -1)
               {
                    return Results.NotFound();
               }
               GameSummaryDto game = new(
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