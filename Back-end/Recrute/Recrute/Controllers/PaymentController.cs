using Microsoft.AspNetCore.Mvc;
using Recrute.Data;
using Recrute.Models;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using Stripe;
using Microsoft.EntityFrameworkCore;
using Stripe.Checkout;

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
        public static double Price = 300.00; // Assuming this is in EUR

        [HttpPost("Payment")]
        public async Task<ActionResult> Pay([FromBody] Payment pay)
        {
            if (pay == null)
            {
                return BadRequest("Payment attributes are null");
            }

            try
            {
                StripeConfiguration.ApiKey = "sk_test_51OWe0KCl1DKyJcVcYnUxR6WSXVSuhGGG4DxvYfdj12fkDPMhBnUr6l2ntZ5I04TdJQhXHutBV5uBBIqCZ25HQr8u00OX1PZOJD";

                // Ensure amount is in the smallest currency unit (EUR -> cents)
                long amountInCents = (long)(pay.totalPrice * 100);

                var paymentIntentService = new PaymentIntentService();
                var paymentIntent = paymentIntentService.Create(new PaymentIntentCreateOptions
                {
                    Amount = amountInCents,
                    Currency = "eur",
                    PaymentMethodTypes = new List<string> { "card" },
                    Description = "Package Purchase",
                });

                Console.WriteLine($"Payment Intent Created: {paymentIntent.Id}");

                var confirmOptions = new PaymentIntentConfirmOptions
                {
                    PaymentMethod = "pm_card_visa", // Stripe test card
                };

              

                var user = _db.user.FirstOrDefault(x => x.username == username);
                if (user != null)
                {
                    var confirmedIntent = paymentIntentService.Confirm(paymentIntent.Id, confirmOptions);
                    Console.WriteLine($"Payment Intent Confirmed: {confirmedIntent.Status}");

                    if (confirmedIntent.Status == "succeeded")
                    {
                    


                        var rec = _db.user.FirstOrDefault(a => a.username == username);
                        
                        if (rec != null)
                        {
                            var r = new RecruteComp
                            {
                                RecrComp = rec.username,
                                Nr_Employ = pay.userCount,

                            };

                            UsingPack u = new UsingPack()
                            {
                                RecrComp = rec.username,
                                Exp_Day = DateTime.Now.Date
                            };

                            _db.usingpack.Add(u);
                            _db.recruteComp.Add(r);
                            _db.SaveChanges();
                        }
                    }
                    else
                    {
                        return BadRequest("Payment was not successful.");
                    }
                }
                else
                {
                    return BadRequest("User not found.");
                }

                return Ok(pay);
            }
            catch (Exception ex)
            {
                return BadRequest($"Stripe error: {ex.Message}");
            }
        }


      
    }
}
