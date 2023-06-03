using Microsoft.AspNetCore.Identity;

public class ChorelyUser : IdentityUser
{
    //Works as FK for "Administrator" accounts
    public string? AssignedAdministratorId { get; set; }
}