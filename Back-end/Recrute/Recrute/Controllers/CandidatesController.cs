using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recrute.Data;
using Recrute.Models;

namespace Recrute.Controllers
{
    public class CandidatesController : Controller
    {
        private readonly string _uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

        private readonly RecruteDbContext db;
        public static string Username { get; set; }

        public CandidatesController(RecruteDbContext db, IWebHostEnvironment env)
        {
            this.db = db;
         

            if (!Directory.Exists(_uploadFolder))
            {
                Directory.CreateDirectory(_uploadFolder);
            }
        }

        [HttpGet("Candidate")]
        public int getCand()
        {
            try
            {
                var comp = db.user.FirstOrDefault(a => a.username == Username);
                var can = db.Candidat.Where(a => a.RecrComp == comp.username).ToList();
                return can.Count();
            }
            catch (Exception)
            {
                return 0;
            }
        }

        [HttpGet("Candidate/List")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var comp = await db.user.FirstOrDefaultAsync(a => a.username == Username);
                if (comp == null)
                    return NotFound("User not found");

                var list = await db.Candidat
                    .Where(a => a.RecrComp == comp.username)
                    .ToListAsync();

                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpPost("Candidate/Create")]
        public async Task<IActionResult>Create([FromForm] Candidate a, IFormFile cv)
        {
            try
            {
                if (a == null || cv == null)
                    return BadRequest("Attributes are nullable");

                var fileName = a.Username + "_CV.pdf";
                var filePath = Path.Combine(_uploadFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await cv.CopyToAsync(stream);
                }

                var newCandidate = new Candidate
                {
                    Username = a.Username,
                    RecrComp = Username,
                    Position = a.Position,
                    Review = a.Review,
                    File_Cv = fileName
                };

                db.Candidat.Add(newCandidate);
                await db.SaveChangesAsync();

                string fileUrl = $"{Request.Scheme}://{Request.Host}/uploads/{fileName}";
                return Ok(new { message = "Candidate created successfully!", url = fileUrl });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Candidate/Update/{username}")]
        public async Task<IActionResult> Update([FromForm] Candidate a, IFormFile? cv)
        {
            try
            {
                var existing = db.Candidat.FirstOrDefault(x => x.Username == a.Username);
                if (existing == null) return NotFound();

                existing.Position = a.Position;
                existing.Review = a.Review;

                if (cv != null)
                {
                    string fileName = a.Username + "_CV.pdf";
                    string filePath = Path.Combine(_uploadFolder, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await cv.CopyToAsync(stream);
                    }
                    existing.File_Cv = fileName;
                }

                db.Candidat.Update(existing);
                await db.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Candidate/Delete/{username}")]
        public async Task<IActionResult> Delete(string username)
        {
            try
            {
                var app = db.Candidat.FirstOrDefault(a => a.Username == username);

                if (app == null)
                    return BadRequest("Candidate does not exist");

                db.Remove(app);
                await db.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
