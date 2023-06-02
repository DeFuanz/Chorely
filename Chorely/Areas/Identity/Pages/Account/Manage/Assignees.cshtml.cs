using System.ComponentModel.DataAnnotations;
using Chorley.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chorely.Areas.Identity.Pages.Account.Manage;

public class ManageAssigneeModel : PageModel
{
    private readonly UserManager<IdentityUser> _userManager;

    public ManageAssigneeModel(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    [BindProperty]
    public InputModel Input { get; set; }

    public IEnumerable<Assignee> Assignees { get; set; }

    public class InputModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
