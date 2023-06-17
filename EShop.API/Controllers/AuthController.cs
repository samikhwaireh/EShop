using Azure.Core;
using EShop.Infrastructure.Identity.Models;
using EShop.Infrastructure.Identity.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace EShop.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly SignInManager<ApplicationUser> signInManager;
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly IAuthService authService;
    private readonly JwtSettings jwtSettings;

    public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, IAuthService authService, IOptions<JwtSettings> jwtSettingsOptions)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.roleManager = roleManager;
        this.authService = authService;
        jwtSettings = jwtSettingsOptions.Value;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest model)
    {
        var result = await signInManager.PasswordSignInAsync(model.Username, model.Password, false, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            var user = await userManager.FindByNameAsync(model.Username);
            var roles = await userManager.GetRolesAsync(user);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, string.Join(",", roles))
            };

            var accessToken = authService.GenerateAccessToken(claims);
            var refreshToken = authService.GenerateRefreshToken();

            Response.Cookies.Append("RefreshToken", refreshToken, new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddMinutes(jwtSettings.RefreshTokenExpirationMinutes),
                SameSite = SameSiteMode.None,
                Secure = true
            });

            return Ok(new
            {
                username = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                JwtToken = accessToken
            });
        }

        return Unauthorized();
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken()
    {
        var refreshToken = Request.Cookies["RefreshToken"];

        if (string.IsNullOrEmpty(refreshToken))
            return Unauthorized();

        var principal = authService.GetPrincipalFromToken(refreshToken);

        if (principal == null)
            return Unauthorized();

        var username = principal.Identity.Name;
        var user = await userManager.FindByNameAsync(username);

        if (user == null)
            return Unauthorized();

        // Generate new access token
        var roles = await userManager.GetRolesAsync(user);
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Role, string.Join(",", roles))
        };
        var accessToken = authService.GenerateAccessToken(claims);

        return Ok(new { AccessToken = accessToken });
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();

        Response.Cookies.Delete("RefreshToken");

        return Ok();
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request, string role = "User")
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (!await roleManager.RoleExistsAsync(role))
        {
            var newRole = new IdentityRole(role);
            await roleManager.CreateAsync(newRole);
        }

        var user = new ApplicationUser
        {
            UserName = request.Username,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
        };

        var result = await userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        await userManager.AddToRoleAsync(user, role);

        return Ok(new
        {
            username = user.UserName,
            email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
        });
    }
}
