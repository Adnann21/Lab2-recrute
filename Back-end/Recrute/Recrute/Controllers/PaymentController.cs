using Microsoft.AspNetCore.Mvc;
using Recrute.Data;
using Recrute.Models;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using Stripe;
using Microsoft.EntityFrameworkCore;

namespace Recrute.Controllers
{
    public class PaymentController : Controller
    {
        private readonly RecruteDbContext _db;
        // GET: LoginController
        public PaymentController(RecruteDbContext dbContext)
        {
            this._db = dbContext;

        }
        public static string username { get; set; }
        public static Double Price { get; set; }



        [HttpPost("Payment")]
        public async Task<ActionResult> Pay([FromBody] Payment pay)
        {
            string random = "";

            if (pay == null)
            {
                return BadRequest("Payment atributes are null");
            }
            else
            {
                try
                {

                    StripeConfiguration.ApiKey = "pk_test_51OWe0KCl1DKyJcVcTPuad4uMinERoYHViKJyJeU80fl2KTlSAYus958iL0TZlq9iC9aJr75xm0pkUhAuhZ65hopV001gQItJiL";


                    // 1. Create a Payment Intent
                    var paymentIntentService = new PaymentIntentService();
                    var paymentIntent = paymentIntentService.Create(new PaymentIntentCreateOptions
                    {
                        Amount = ((int)Price) * 100, // Amount in the smallest currency unit (e.g., 5000 = $50.00)
                        Currency = "eur",
                        PaymentMethodTypes = new List<string> { "card" },
                        Description = "Package Purchase",
                    });

                    Console.WriteLine($"Payment Intent Created: {paymentIntent.Id}");

                    // 2. Confirm the Payment Intent (if required, e.g., for manual confirmation)
                    var confirmOptions = new PaymentIntentConfirmOptions
                    {
                        PaymentMethod = "pm_card_visa", // Replace with your actual PaymentMethod ID
                    };

                    var confirmedIntent = paymentIntentService.Confirm(paymentIntent.Id, confirmOptions);
                    Console.WriteLine($"Payment Intent Confirmed: {confirmedIntent.Status}");

                    string secureRandomString = new string(Enumerable.Range(0, 10)
                .Select(_ => "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"[
                 RandomNumberGenerator.GetInt32(62)]).ToArray());
                    random = secureRandomString;



                    var user = _db.user
                                      .Where(x => x.username == username)
                                      .FirstOrDefault();
                    if (confirmedIntent.Status == "succeeded" && user != null)
                    {


                        string hash = BCrypt.Net.BCrypt.HashPassword(random);

                        user.Password = hash;
                        _db.user.Update(user);
                        _db.SaveChanges();
                        SendEmailWithAttachment(pay.Email, random);

                        var rec = _db.employ.Where(a => a.Username == username).FirstOrDefault();
                        UsingPack u = new UsingPack()
                        {
                            RecrComp = rec.RecrComp,
                            Exp_Day = DateTime.Now.Date
                        };

                        _db.usingpack.Add(u);
                        _db.SaveChanges();

                    }


                    else
                    {
                        return BadRequest("Errorr");
                    }




                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return Ok(pay);
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
