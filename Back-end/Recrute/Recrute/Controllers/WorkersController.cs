using Microsoft.AspNetCore.Mvc;
using Recrute.Models;
using Recrute.Data;
using DocumentFormat.OpenXml.Office.PowerPoint.Y2021.M06.Main;
using System.Security.Policy;
namespace Recrute.Controllers
{
    public class WorkersController : Controller
    {
        RecruteDbContext db;
        public static string Username {  get; set; }

        public WorkersController(ref RecruteDbContext db)
        {
            this.db = db;
        }

        [HttpGet("Works/List")]
        public async Task<IActionResult> Get()
        {
            var get = db.user.Where(a=>a.username==Username).FirstOrDefault();
            if (get == null)
            {
                return NotFound();
            }
            var list=db.workers.Where(a=>a.Emp_Username==Username).ToList();
            if (list == null)
            {
                return BadRequest("List is null");
            }
            return View(list);
        }


        [HttpPost("Works/Create")]
       public async Task<IActionResult> Create (Workers w)
        {
            try {
                if (w == null)
                {
                    return BadRequest("Attributes are null");
                }
                else
                {
                    var app= db.workers.Where(a=>a.Ap_Username==w.Ap_Username).FirstOrDefault();
                    if(app == null)
                    {
                        Workers d = new Workers() { 
                            Ap_Username=w.Ap_Username,
                            CompName=w.CompName,
                            Emp_Username=Username,
                            Payment=w.Payment,
                            Pozition=w.Pozition,
                        
                        
                        };
                        db.workers.Add(d);
                        db.SaveChanges();

                        return Ok();
                    }
                    else
                    {
                        return BadRequest("Applicant is in a meeting with a company");
                    }
                }

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }



        [HttpPut("Workers/Update")]
        public async Task<IActionResult> Update(String CompName, String AppName,Workers w)
        {
            try
            {

                var work= db.workers.Where(a=>a.CompName==CompName&&a.Ap_Username==AppName).FirstOrDefault();
                if (work == null)
                {
                    return BadRequest("This agreement doesn't egzist");
                }
                else
                {
                    work.Pozition = w.Pozition;
                    work.Payment = w.Payment;
                   db.workers.Update(work);
                    db.SaveChanges();
                    return Ok(new { Message = "This work aggrement updated sucsessfully " });

                }

                        
                               
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        
        }

        [HttpDelete("Works/Delete")]
        public async Task<IActionResult> Delete(String CompName,String AppName)
        {
            try
            {
                var work= db.workers.Where(a=>a.CompName==CompName&& a.Ap_Username==AppName && a.Emp_Username==Username).FirstOrDefault();
                
                if (work == null)
                {
                    return BadRequest("This agreement doesn't egzist");
                }
                else
                {
                    db.workers.Remove(work);
                    db.SaveChanges();
                    return Ok(new { Message = " this agreement is deleted sucesfuly" });
                }
            
            
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }
    }
}
