using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Chorely.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Chorely.Controllers;

[AllowAnonymous]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly SignInManager<ChorelyUser> _signInManager; 

    public HomeController(ILogger<HomeController> logger, SignInManager<ChorelyUser> signInManager)
    {
        _logger = logger;
        _signInManager = signInManager;
    }

    public IActionResult Index()
    {
        if (_signInManager.IsSignedIn(User))
        {
            return LocalRedirect("~/Chore");
        }
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
