namespace WebApplication1.Dtos;
using System.ComponentModel.DataAnnotations;
public record class CreateGameDto(
    [Required(ErrorMessage = "Name is required")]string Name,
    [Required(ErrorMessage = "Genre is required")]string Genre,
    [Required(ErrorMessage = "Price is required")]decimal? Price,
    [Required(ErrorMessage = "Release date is required")]DateOnly? ReleaseDate
);