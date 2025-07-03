using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Recrute.Models;
using Recrute.Data;
using System.ComponentModel.DataAnnotations;
using ExportToExcelService.Controllers;
using Recrute.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly RecruteDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthController(RecruteDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }


    [HttpGet("Users")]
    public async Task<IActionResult> GetUsers()
    {

        var users = _context.user.Where(x => x.Role != 0).ToList();

        if (users == null || users.Count == 0)
        {
            return NotFound(new { Message = "No users found." });
        }

        return Ok(users);
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] Users user)
    {
        if (await _context.user.AnyAsync(u => u.username == user.username))
            return BadRequest("Username already exists");

        user.Role = 2;
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        _context.user.Add(user);
        await _context.SaveChangesAsync();

        return Ok("User registered successfully");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto login)
    {
        var user = await _context.user.FirstOrDefaultAsync(u => u.username == login.Username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(login.Password, user.Password))
            return Unauthorized("Invalid username or password");

        var accessToken = GenerateJwtToken(user);
        var refreshToken = GenerateRefreshToken();

        var rt = new RefreshToken
        {
            Token = refreshToken,
            Username = user.username,
            ExpiryDate = DateTime.Now.AddHours(0.5)
        };
        
        var b = _context.user.First(u => u.username == login.Username);

        var pay = _context.usingpack.Where(u => u.RecrComp == b.username).FirstOrDefault();
        // Perform login logic (you might want to return tokens, etc. in an API)
        if (pay == null)
        {

            return BadRequest("Company doesn't have any payment");
        }
        else
        {
            if (pay.Exp_Day.AddYears(1) == DateTime.Now)
            {
                return BadRequest("Company has to pay");
            }


            else if (pay.Exp_Day.AddYears(1) <= DateTime.Now)
            {
                return BadRequest("Company has to pay");

            }


            else if (pay.Exp_Day.AddYears(1) >= DateTime.Now)
            {

                PaymentController.username = login.Username;
                EmployController.CompanyName = login.Username;
                ExportController.CompName = login.Username;
                PasswordController.username = login.Username;
                CandidatesController.Username = login.Username;
                CompanyController.RecruteComp = login.Username;

                

            }
            else
            {
                return Unauthorized("Invalid User");
            }
        }

            _context.RefreshTokens.Add(rt);
        await _context.SaveChangesAsync();



        return Ok(new
        {
            accessToken,
            refreshToken,
            
            role = user.Role
        });
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
    {
        var token = await _context.RefreshTokens.FirstOrDefaultAsync(t => t.Token == refreshToken);
        if (token == null || token.ExpiryDate < DateTime.UtcNow)
            return Unauthorized("Invalid or expired refresh token");

        var user = await _context.user.FirstOrDefaultAsync(u => u.username == token.Username);
        if (user == null)
            return Unauthorized("Invalid refresh token");

        var newAccessToken = GenerateJwtToken(user);
        var newRefreshToken = GenerateRefreshToken();

        _context.RefreshTokens.Remove(token);
        _context.RefreshTokens.Add(new RefreshToken
        {
            Token = newRefreshToken,
            Username = user.username,
            ExpiryDate = DateTime.Now.AddHours(0.5)
        });

        await _context.SaveChangesAsync();

        return Ok(new
        {
            accessToken = newAccessToken,
            refreshToken = newRefreshToken
        });
    }

    private string GenerateJwtToken(Users user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.username),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(0.5),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(256));
    }
}

public class LoginDto
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
}