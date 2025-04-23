using Microsoft.AspNetCore.Mvc;
using Recrute.Data;
using Recrute.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Net;

namespace Recrute.Controllers
{
  
    public class PasswordController : Controller
    {

        RecruteDbContext db;
        public static string username {  get; set; }
        public PasswordController(RecruteDbContext db)
        {
            this.db = db;
        }

        [HttpPost("Password")]
        public async Task<IActionResult> ChangePassword([FromBody] Password p)
        {
            try {

                var user = db.user.FirstOrDefault(x=>x.username==username);
                if (user == null)
                {
                    return BadRequest("Username not valid");
                }
                else if (BCrypt.Net.BCrypt.Verify(p.OldPassword,user.Password)) {

                if (p.NewPassword != p.ConfirmPassword)
                    {
                        return BadRequest("Passwor not match");
                    } 
                else
                    {
                        string hash = BCrypt.Net.BCrypt.HashPassword(p.NewPassword);

                        user.Password = hash;

                        db.user.Update(user);
                        db.SaveChanges();

                        SendEmailWithAttachment(user.Email);
                        return Ok();

                    }                
                }
             
                return Ok(p);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        private void SendEmailWithAttachment(string email)
        {
            string senderEmail = "eriongashi353@gmail.com";
            string senderPassword = "acmt whaa aqwu cows"; //dpka hdba eoqh dxbz  
            string recipientEmail = email;
            string subject = "Password";
            string message = "Your password has been change suksesfuly";

            try
            {
                using (System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient("smtp.gmail.com"))
                {
                    smtpClient.Port = 587;
                    smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);
                    smtpClient.EnableSsl = true;

                    using (MailMessage mail = new MailMessage(senderEmail, recipientEmail, subject, message))
                    {
                       
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
