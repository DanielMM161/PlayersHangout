namespace Backend.Src.DTOs.Genre;

using Backend.Src.DTOs;
using Backend.Src.Models;
using System.ComponentModel.DataAnnotations;

public class GenreCreateDTO : BaseDTO<Genre>
{
    [Required]
    public string Name { get; set; } = null!;

    public override void UpdateModel(Genre model)
    {
        model.Name = Name;
    }
}