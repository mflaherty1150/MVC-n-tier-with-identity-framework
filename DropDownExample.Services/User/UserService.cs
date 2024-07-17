using DropDownExample.Data;
using DropDownExample.Data.Entities;
using DropDownExample.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DropDownExample.Services.User;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<UserEntity> _userManager;
    private readonly SignInManager<UserEntity> _signInManager;

    public UserService(
        ApplicationDbContext context, 
        UserManager<UserEntity> userManager, 
        SignInManager<UserEntity> signInManager) 
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<bool> RegisterUserAsync(UserRegister model)
    {
        if (await UserExistsAsync(model.Email, model.Username))
            return false;

        UserEntity user = new()
        {
            UserName = model.Username,
            Email = model.Email
        };

        var createResult = await _userManager.CreateAsync(user, model.Password);
        return createResult.Succeeded;
    }

    public async Task<bool> LoginAsync(UserLogin model)
    {
        var user = await _userManager.FindByNameAsync(model.Username);
        if (user is null)
            return false;

        var isValidPassword = await _userManager.CheckPasswordAsync(user, model.Password);
        if (isValidPassword == false)
            return false;

        await _signInManager.SignInAsync(user, true);
        return true;
    }

    public async Task LogoutAsync() => await _signInManager.SignOutAsync();

    private async Task<bool> UserExistsAsync(string email, string username)
    {
        var normalizedEmail = _userManager.NormalizeEmail(email);
        var normalizedUsername = _userManager.NormalizeName(username);

        return await _context.Users.AnyAsync(u =>
            u.NormalizedEmail == normalizedEmail ||
            u.NormalizedUserName == normalizedUsername
        );
    }
}