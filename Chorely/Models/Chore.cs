using System.ComponentModel.DataAnnotations;

namespace Chorely.Models;

public class Chore
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Assigned Date")]
    public DateTime AssignedDate { get; set; }
    public bool Completed { get; set; } = false;
    public decimal Value { get; set; }
    public string? Notes { get; set; }

    //FK to admin and assignees
    [Display(Name = "Created By")]
    public string? CreatedById { get; set; }
    [Display(Name = "Assigned to")]
    public string? AssignedToId { get; set; }
}