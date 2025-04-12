using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recrute.Data;
using Recrute.Models;

namespace Recrute.Controllers
{
    public class CompanyController : Controller
    {

        public static string RecruteComp {  get; set; }

        RecruteDbContext db;
        public CompanyController( RecruteDbContext db) {
            db = db;
        }
    

        [HttpGet("Company/List")]
       public async Task<IActionResult> Get()
        {
            var comp =await db.Comp.Where(x => x.RecruteComp== RecruteComp).ToListAsync();
            if (comp == null)
            {
                return BadRequest(new { Message = "Null" });
            }
            return Ok(comp);
        }

        [HttpPost("Company/Create")]
        public async Task<IActionResult> Create([FromBody]Company c)
        {
            try
            {
                if (c!=null)
                {
                    Company comp = new Company()
                    {
                        CompanyName = c.CompanyName,
                        RecruteComp = RecruteComp,
                        Location = c.Location,
                    };
                    db.Comp.Add(comp);
                    db.SaveChanges();
                    return Ok(new { Message = "Company Created successfully." });
                   
                }
                else
                {
                    return BadRequest("Company atributes are null");
                }
               
            }
            catch (Exception ex)
            {
               return BadRequest(ex.Message);    
            }
        }

        [HttpPut("Company/Update/{CompName}")]
        public async Task<IActionResult> Edit(string CompName, [FromBody] Company e)
        {
            try
            {

                var emp = db.Comp.Where(x=>x.RecruteComp==RecruteComp).FirstOrDefault(e => e.CompanyName == CompName);

                if (e == null)
                {
                    return NotFound();
                }
                else if (CompName == null)
                {
                    return BadRequest("Username null");
                }
                else if (emp == null)
                {
                    return BadRequest("Employ is not valid.");
                }
                else
                {
                    emp.CompanyName = e.CompanyName;
                    emp.Location = e.Location;
                    

                    db.Comp.Update(emp);
                    db.SaveChanges();

                    return Ok(new { Message = "Company updated successfully." });
                }


            }
            catch (Exception ex) { return BadRequest(ex.Message); }


        }

        [HttpDelete("Delete/Company/{CompName}")]
        public async Task<IActionResult> DeleteConfirmed(string CompName)
        {
            try
            {
                var emp = db.Comp.FirstOrDefault(e => e.CompanyName == CompName);


                if (CompName == null)
                {
                    return BadRequest("Company name null");
                }
                else if (emp == null)
                {
                    return BadRequest("Employ is not valid.");
                }
                else
                {
                    db.Comp.Remove(emp);
                    await db.SaveChangesAsync();
                    return Ok(new { Message = "Company deleted successfully." });
                }

            }
            catch (Exception ex) { return BadRequest(ex.Message); }


        }
    }
}
