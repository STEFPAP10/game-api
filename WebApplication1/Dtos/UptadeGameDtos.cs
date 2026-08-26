namespace WebApplication1.Dtos;
using System.ComponentModel.DataAnnotations;
public record class UpdateGameDto(
    [Required(ErrorMessage = "Name is required")] [StringLength(50, ErrorMessage = "Name must be at most 50 characters long")]
    string Name,
    [Required(ErrorMessage = "Genre is required")] [StringLength(20, ErrorMessage = "Genre must be at most 20 characters long")]
    string Genre,
    [Required(ErrorMessage = "Price is required")]decimal? Price,
    [Required(ErrorMessage = "Release date is required")]DateOnly? ReleaseDate
);