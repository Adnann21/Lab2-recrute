using Microsoft.AspNetCore.Mvc;
using Recrute.Models;
using Recrute.Data;
using Microsoft.EntityFrameworkCore;
namespace Recrute.Controllers
{
    public class SignUpController : Controller
    {
        private readonly RecruteDbContext _db;
        
        public SignUpController(RecruteDbContext dbContext)
        {
            _db = dbContext;
        }

        // POST: api/signup

        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] Users user)
        {
            if (user == null)
            {
                return BadRequest("User data is required.");
            }

            try
            {
                // Hash the password using BCrypt
                string hash = BCrypt.Net.BCrypt.HashString(user.Password);

                // Create new user
                var newUser = new Users
                {
                    username = user.username,
                    Email = user.Email,
                    Password = hash,
                    Role = 0
                };

                // Optional: Create related RecruteComp object
                /*var r = new RecruteComp
                {
                    RecrComp = user.Username,
                    Nr_Employ = 0
                };*/

                // Add user and optionally RecruteComp
                _db.user.Add(newUser);
                // _db.recruteComp.Add(r); // Uncomment if needed

                await _db.SaveChangesAsync();

                // Return created result
                return CreatedAtAction(nameof(Signup), new { Username = newUser.username }, newUser);
            }
            catch (DbUpdateException dbEx)
            {
                // Log EF-specific error
                Console.WriteLine($"DB Update Error: {dbEx.InnerException?.Message ?? dbEx.Message}");
                return StatusCode(500, "A database error occurred.");
            }
            catch (Exception ex)
            {
                // Log any other unexpected error
                Console.WriteLine($"General Error: {ex.Message}");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

    }
}
