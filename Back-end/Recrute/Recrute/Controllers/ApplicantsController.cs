using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recrute.Data;

using Recrute.Models;

namespace Recrute.Controllers
{
    public class ApplicantsController : Controller
    {

        private readonly string _uploadFolder = "/CV";
        RecruteDbContext db;
        public static String Username  /*{  get; set; }*/= "string";

        public ApplicantsController(RecruteDbContext db)
        {
            this.db = db;
        }

        [HttpGet("Applicant/List")]
        public async Task<IActionResult> Get()
        {
            
            var comp = db.user.Where(a=>a.username == Username).FirstOrDefault();
            var rec= db.employ.Where(a=>a.Username==comp.username).FirstOrDefault();
            var list = await db.applicants.Where(a=>a.RecrComp==rec.RecrComp).ToListAsync();
            if(list==null)
            {
                return BadRequest("List of applicants is null"); 
             
            }
            else {
                  return Ok(list);
            }
            
        }

        [HttpPost("Applicant/Create")]
        public async Task<IActionResult> Create([FromBody] Models.Applicants a, IFormFile cv)
        {
            try
            {


                if(a==null || cv==null)
                {
                    return BadRequest("Atributes are nullable");
                }
                else
                {
                    Models.Applicants  b=new Models.Applicants() { 
                        Username = a.Username,
                        RecrComp = a.RecrComp,
                        Position = a.Position,
                        Review=a.Review,
                        File_Cv=a.Username+"_CV",
                    
                    
                    
                    };

                    string uniqueFileName = a.Username+"_CV";
                    string filePath = Path.Combine(_uploadFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await cv.CopyToAsync(stream);
                    }
                    db.applicants.Add(b);
                    db.SaveChanges();
                    // Return success with file URL
                    string fileUrl = $"{Request.Scheme}://{Request.Host}/uploads/{uniqueFileName}";
                    return Ok(new { message = "File uploaded successfully!", url = fileUrl });
                 
                }


                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            
        }

        [HttpPut("Applicants/Update/{username}")]
        public async Task<IActionResult> Update(string username, [FromBody]Applicants a)
        {
            try
            {
                if (username == null)
                {
                    return BadRequest("Atributes are null");
                }
                else if (a == null)
                {
                    return BadRequest("Atributes are null");
                }
                else
                {
                    var b= db.applicants.Where(a=>a.Username==username).FirstOrDefault();

                    b.RecrComp = a.RecrComp;
                    b.Position = a.Position;
                    b.Review = a.Review;


                    db.Update(b);
                    db.SaveChanges(true);
                    return Ok();
                }


                
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

         
        }


        [HttpDelete("Applicant/Delete/{username}")]
        public async Task<IActionResult> Delete(String username)
        {

            try
            {
                if (username == null)
                {
                    return BadRequest("Attributes are nullable");
                }
                else
                {
                    var app = db.applicants.Where(a => a.Username == username).FirstOrDefault();

                    if (app == null)
                    {
                        return BadRequest("Applicant does not exist");
                    }
                    else
                    {
                        db.Remove(app);
                        db.SaveChanges(true);
                        return Ok();

                    }
                }
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
