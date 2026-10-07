using Librarium.API.Contacts;
using Librarium.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Librarium.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AuthController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var user = new ApplicationUser
        {
            UserName = request.Name
        };

        var result = await _userManager.CreateAsync(
            user,
            request.Password);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        await _signInManager.SignInAsync(
            user,
            isPersistent: true);

        return Ok(new
        {
            message = "User created and logged in",
            userId = user.Id,
            name = user.UserName
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.Name);

        if (user == null)
        {
            return Unauthorized("Invalid username or password");
        }

        var result = await _signInManager.PasswordSignInAsync(
            user,
            request.Password,
            isPersistent: true,
            lockoutOnFailure: false);

        if (!result.Succeeded)
        {
            return Unauthorized("Invalid username or password");
        }

        return Ok(new
        {
            message = "Logged in",
            userId = user.Id,
            name = user.UserName
        });
    }

    [Authorize]
    [HttpGet("me")]
    public IActionResult GetCurrentUser()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var userName = User.FindFirstValue(ClaimTypes.Name);

        return Ok(new
        {
            userId,
            name = userName
        });
    }
}