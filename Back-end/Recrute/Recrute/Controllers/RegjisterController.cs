using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Recrute.Models;
using Recrute.Data;

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

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] Users user)
    {
        if (await _context.user.AnyAsync(u => u.username == user.username))
            return BadRequest("Username already exists");
        
        
        user.Role = 2;
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password); // Secure password
        _context.user.Add(user);
        await _context.SaveChangesAsync();

        return Ok("User registered successfully");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] Users login)
    {
        var user = await _context.user.FirstOrDefaultAsync(u => u.username == login.username);

        if (user == null || !BCrypt.Net.BCrypt.Verify(login.Password, user.Password))
            return Unauthorized("Invalid username or password");

        var token = GenerateJwtToken(user);
        return Ok(new { token });
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
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
