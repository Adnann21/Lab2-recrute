using Microsoft.AspNetCore.Mvc;
using Recrute.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recrute.Models;
using System.Security.Cryptography;
using System;
using Recrute.Migrations;
using System.Net.Mail;
using System.Net;
namespace Recrute.Controllers
{
    public class EmployController : Controller
    {
        private readonly RecruteDbContext _context;
        public static string CompanyName {  get; set; }
        public EmployController(RecruteDbContext context)
        {
            _context = context;
        }
      
        [HttpGet("Employ/List")]
        public async Task<ActionResult> GetAllEmploy()
        {
            var vi = await _context.employ.ToListAsync();
            if (vi == null)
            {
                return BadRequest(new { Message = "Null" });
            }
            return Ok(vi);
        }

        [HttpPost("Employ/Create")]
        public async Task<IActionResult> Create([FromBody] Models.Employ d)
        {
            try
            {
                if (d == null)
                {
                    return BadRequest("Input null");
                }

                var empCount = _context.employ.Where(e => e.RecrComp == d.RecrComp).Count();
                var comp = _context.recruteComp.FirstOrDefault(c => c.RecrComp == d.RecrComp);

                if (comp == null)
                {
                    return BadRequest("Company not found");
                }
                else if (empCount > comp.Nr_Employ)
                {
                    return BadRequest("More employees than allowed");
                }
                else
                {
                    string secureRandomString = new string(Enumerable.Range(0, 10)
                                    .Select(_ => "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"[
                                     RandomNumberGenerator.GetInt32(62)]).ToArray());
                   string random = secureRandomString;

                    string hash = BCrypt.Net.BCrypt.HashPassword(random);

                    Users u = new Users()
                    {
                        username = d.Username,
                        Email = d.Email,
                        
                        Role = 2,
                        
                        Password = hash,
                      


                    };
                    SendEmailWithAttachment(d.Email, random);

                    _context.user.Add(u);
                    _context.employ.Add(d);
                    _context.SaveChanges();
                    return CreatedAtAction(nameof(Models.Employ), new { username = d.Username }, d);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }
        [HttpPut("Employ/Update/{username}")]
        public async Task<IActionResult> Edit(string username, [FromBody]Models.Employ e)
        {
            try { 
                var emp= _context.employ.FirstOrDefault(e=>e.Username == username);

                if (e == null)
                {
                    return NotFound();
                }
                else if (username == null)
                {
                    return BadRequest("Username null");
                }
                else if (emp==null)
                {
                    return BadRequest("Employ is not valid.");
                }
                else
                {
                    emp.Email = e.Email;
                    emp.Region = e.Region;

                    _context.employ.Update(emp);
                    _context.SaveChanges(true);

                    return Ok(new { Message = "Employ updated successfully." });
                }

            
            }            
            catch (Exception ex) { return BadRequest(ex.Message); }

           
        }

        [HttpDelete("Delete/Employ/{username}")]
        public async Task<IActionResult> DeleteConfirmed(string username)
        {
            try
            {
                var emp = _context.employ.FirstOrDefault(e => e.Username == username);

               
                if (username == null)
                {
                    return BadRequest("Username null");
                }
                else if (emp == null)
                {
                    return BadRequest("Employ is not valid.");
                }
                else
                {
                    _context.employ.Remove(emp);
                    await _context.SaveChangesAsync();
                    return Ok(new { Message = "Employ deleted successfully." });
                }
               
            }
            catch (Exception ex) { return BadRequest(ex.Message); }


        }


        private void SendEmailWithAttachment(string email, string Pass)
        {
            string senderEmail = "eriongashi353@gmail.com";
            string senderPassword = "acmt whaa aqwu cows"; //dpka hdba eoqh dxbz  
            string recipientEmail = email;
            string subject = "Password";
            string message = "Attached with this email you can finde your acc passworde. \n Also you can change that password after you login in your acc \n \n Your acc Passworde:" + Pass;

            try
            {
                using (System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient("smtp.gmail.com"))
                {
                    smtpClient.Port = 587;
                    smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);
                    smtpClient.EnableSsl = true;

                    using (MailMessage mail = new MailMessage(senderEmail, recipientEmail, subject, message))
                    {
                        // Attach the PDF file
                        /* if (!string.IsNullOrEmpty(attachmentPath))
                         {
                             Attachment attachment = new Attachment(attachmentPath);
                             mail.Attachments.Add(attachment);
                         }
 */
                        smtpClient.Send(mail);
                        Console.WriteLine("Email sent successfully!");
                    }
                }
            }
            catch (SmtpException ex)
            {
                Console.WriteLine($"SMTP error occurred: {ex.StatusCode} - {ex.Message}", "Error");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}", "Error");
            }
        }


    }
}
