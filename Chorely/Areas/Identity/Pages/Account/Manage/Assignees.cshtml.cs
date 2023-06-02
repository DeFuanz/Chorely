using Chorley.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chorely.Areas.Identity.Pages.Account.Manage;

public class AssigneesPage : PageModel
{
    private readonly ChorleyContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleStore;

    public AssigneesPage(ChorleyContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleStore)
    {
        _context = context;
        _userManager = userManager;
        _roleStore = roleStore;
    }


}