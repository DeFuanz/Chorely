using System.ComponentModel.DataAnnotations;

namespace Chorely.Models;

public class Chore
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }

    [DataType(DataType.Date)]
    public DateTime AssignedDate {get; set;}
    public bool Completed { get; set; } = false;
    public decimal Value { get; set; }
    public string? Notes { get; set; }
}