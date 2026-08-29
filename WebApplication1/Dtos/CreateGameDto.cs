namespace WebApplication1.Dtos;
using System.ComponentModel.DataAnnotations;
public record class CreateGameDto(
    [Required(ErrorMessage = "Name is required")] [StringLength(50, ErrorMessage = "Name must be at most 50 characters long")]
    string Name,
    int GenreId,
    [Required(ErrorMessage = "Price is required")]decimal? Price,
    [Required(ErrorMessage = "Release date is required")]DateOnly? ReleaseDate
);