using Microsoft.EntityFrameworkCore;
using WebApplication1.Entities;

namespace WebApplication1.Data;

public class GameStoreContext(DbContextOptions<GameStoreContext> options) 
    : DbContext(options)
{

    // Οι πίνακες της βάσης δεδομένων μας
    public DbSet<Game> Games => Set<Game>();
    public DbSet<Genre> Genres => Set<Genre>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>().HasData(
            new  { Id = 1, Name = "Action" },
            new  { Id = 2, Name = "Adventure" },
            new  { Id = 3, Name = "RPG" },
            new  { Id = 4, Name = "Strategy" },
            new  { Id = 5, Name = "Simulation" }
        );
    }
}