using DocumentFormat.OpenXml.Office.CoverPageProps;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recrute.Data;
using Recrute.Models;

namespace Recrute.Controllers
{
    public class JobsController : Controller
    {

        RecruteDbContext _db;
        public static String Username {  get; set; }

        public JobsController(RecruteDbContext db)
        {
            _db = db;
        }

        [HttpGet("Jobs/List")]
        public async Task<IActionResult> Get()
        {
            var user = _db.user?.FirstOrDefault(a => a.username == Username);

            if (user == null)
            {
                return BadRequest("User not found");
            }

            var list = await _db.jobs
                                .Where(b => b.RecrComp == user.username && b.DataExp>=DateTime.Now)
                                .ToListAsync();

            if (list == null || !list.Any())
            {
                return BadRequest("No jobs found");
            }
            else
            {
                return Ok(list);
            }
        }

        [HttpPost("Jobs/Create")]
        public async Task<IActionResult> Create([FromBody]Jobs j)
        {
            try
            {
                if(j == null)
                {
                    return BadRequest("Atributes are null");
                }
                else
                {
                    var user = _db.user?.FirstOrDefault(a => a.username == Username);
                    Jobs b= new Jobs()
                    {
                        Pozition = j.Pozition,
                        CompName = j.CompName,
                        CompLocation = j.CompLocation,
                        //RecrComp = user.RecrComp,
                        DataExp = j.DataExp,

                    };
                    _db.jobs.Add(b);
                    _db.SaveChanges();
                   
 return View(j);
                    
                }

                
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        [HttpPut("Job/Update")]
        public async Task<IActionResult> Update(String CompName,String Pozition,[FromBody]Jobs j)
        {
            try
            {
                var job = await _db.jobs.Where(a => a.CompName == CompName && a.Pozition == Pozition).FirstOrDefaultAsync();
                if (job == null)
                {
                    return BadRequest("Jobe does not exist");
                }
                else
                {
                    job.DataExp = j.DataExp;
                    job.CompLocation = j.CompLocation;
                    
                    _db.Update(job);
                    _db.SaveChanges(true);

                return Ok(new { Message = "Job Pozition updated sucessfuly" });

                }

                
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        
        
        
        }


        [HttpDelete("Jobs/Delete")]
        public async Task<IActionResult> Delete(String CompName,String Pozition)
        {
            try
            {
                var job= await _db.jobs.Where(a=>a.CompName==CompName&& a.Pozition==Pozition).FirstOrDefaultAsync();
                    if(job == null)
                {
                    return NotFound();
                }
                else
                {
                    _db.jobs.Remove(job);
                    _db.SaveChanges(true);
                     return Ok();
                }
               
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
