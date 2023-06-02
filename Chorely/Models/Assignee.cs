using Microsoft.AspNetCore.Identity;

public class Assignee : IdentityUser
{
    //Any assignee specific properties can be assigned here

    //Navigation properties and FK for Administrator in 1-M relationship
    public string AdministratorId { get; set; } = default!;
    public Administrator Administrator { get; set; } = default!;
}