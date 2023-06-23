using System.ComponentModel.DataAnnotations;
using Chorley.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Chorely.Areas.Identity.Pages.Account.Manage;

public class ManageAssigneeModel : PageModel
{
    private readonly UserManager<ChorelyUser> _userManager;
    private readonly IUserStore<ChorelyUser> _userStore;
    private readonly RoleManager<IdentityRole> _roleManager;

    public ManageAssigneeModel(UserManager<ChorelyUser> userManager, IUserStore<ChorelyUser> userStore, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _userStore = userStore;
        _roleManager = roleManager;
    }

    [BindProperty]
    public InputModel Input { get; set; }

    public IEnumerable<ChorelyUser> Assignees { get; set; }

    public class InputModel
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }

    public async void OnGet()
    {
        var adminId = _userManager.GetUserId(User);
        Assignees = await _userManager.Users.Where(a => a.AssignedAdministratorId == adminId).ToListAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            var user = CreateUser();

            user.AssignedAdministratorId =  _userManager.GetUserId(User);

            await _userStore.SetUserNameAsync(user, Input.Username, CancellationToken.None);
            var result = await _userManager.CreateAsync(user, Input.Password);

            if (result.Succeeded)
            {
                var userId = await _userManager.GetUserIdAsync(user);
                await EnsureRole(_roleManager, _userManager, userId, "Assignee");

                return RedirectToPage("./Assignees");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        // If we got this far, something failed, redisplay form
        return Page();
    }

    private ChorelyUser CreateUser()
    {
        try
        {
            return Activator.CreateInstance<ChorelyUser>();
        }
        catch
        {
            throw new InvalidOperationException($"Can't create an instance of '{nameof(ChorelyUser)}'. " +
                $"Ensure that '{nameof(ChorelyUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
        }
    }

    private static async Task<IdentityResult> EnsureRole(RoleManager<IdentityRole> roleStore, UserManager<ChorelyUser> usersManager,
                                                              string uid, string role)
    {
        var roleManager = roleStore;

        if (roleManager == null)
        {
            throw new Exception("roleManager null");
        }

        IdentityResult IR;
        if (!await roleManager.RoleExistsAsync(role))
        {
            IR = await roleManager.CreateAsync(new IdentityRole(role));
        }

        var userManager = usersManager;

        //if (userManager == null)
        //{
        //    throw new Exception("userManager is null");
        //}

        var user = await userManager.FindByIdAsync(uid);

        if (user == null)
        {
            throw new Exception("The testUserPw password was probably not strong enough!");
        }

        IR = await userManager.AddToRoleAsync(user, role);

        return IR;
    }
}
