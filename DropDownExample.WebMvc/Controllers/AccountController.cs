using DropDownExample.Models.User;
using DropDownExample.Services.User;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace DropDownExample.WebMvc.Controllers;

public class AccountController : Controller
{
    private readonly IUserService _userService;
    
    public AccountController(IUserService userService)
    {
        _userService = userService;
    }

    // GET Action for Register -> Returns the view to the user
    public IActionResult Register()
    {
        return View();
    }

    // POST Action for Register -> When the user submits their data from the view
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(UserRegister model)
    {
        // First validate the request model, reject if invalid
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // Try to register the user, reject if failed
        var registerResult = await _userService.RegisterUserAsync(model);
        if (registerResult == false)
        {
            // TODO: Add error to page
            return View(model);
        }

        // Login the new user, redirect to home after
        UserLogin loginModel = new()
        {
            Username = model.Username,
            Password = model.Password
        };
        await _userService.LoginAsync(loginModel);
        return RedirectToAction("Index", "Home");
    }

    // GET Login
    public IActionResult Login()
    {
        return View();
    }

    // POST Login
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(UserLogin model)
    {
        var loginResult = await _userService.LoginAsync(model);
        if (loginResult == false)
        {
            // TODO: Add invalid password/username message
            return View(model);
        }

        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Logout()
    {
        await _userService.LogoutAsync();
        return RedirectToAction("Index", "Home");
    }
}