using Microsoft.AspNetCore.Identity;

public class Administrator : IdentityUser
{
    //Any needed admin properties can be assigned here

    //Navigation properties for assignees in M-1 relationship
    public List<Assignee> Assignees { get; set; } = default!;
}