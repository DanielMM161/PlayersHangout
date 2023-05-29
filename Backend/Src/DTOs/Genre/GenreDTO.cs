namespace Backend.Src.DTOs;

using System.ComponentModel.DataAnnotations;

public class GenreDTO
{
    public Guid Id { get; set; }    
    public string Name { get; set; } = null!;
}