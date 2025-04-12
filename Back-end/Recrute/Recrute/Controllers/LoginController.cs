
﻿using Microsoft.AspNetCore.Mvc;
using Recrute.Models;
using Recrute.Data;
using Recrute.Migrations;
using ExportToExcelService.Controllers;
using System.ComponentModel;

namespace Recrute.Controllers
{
    public class LoginController : Controller
    {
        private readonly RecruteDbContext _db;
        // GET: LoginController
        public LoginController(RecruteDbContext dbContext)
        {
            this._db = dbContext;

        }


        // POST: LoginController/Create
        [HttpGet]

        [HttpPost]

        public bool AreCredentialsValid(string username, string password)
        {
            var user = _db.user.SingleOrDefault(u => u.username == username);

            if (user != null)
            {
                // Debugging: Log the hashed password from the database
                Console.WriteLine($"Stored Password: {user.Password}");
           
                // Use a secure password hashing mechanism (e.g., BCrypt)
             
                        if (BCrypt.Net.BCrypt.Verify(password, user.Password))
                        {
                            return true; // Valid username and password
                        }
                        else
                        {
                            // Debugging: Log that the passwords didn't match
                            Console.WriteLine("Passwords did not match");
                        }
                    
                
            }

            return false;
        }
        [HttpPost("login")]
        public async Task<ActionResult> Login(String Username,String  password )
        {
            if (Username == null || string.IsNullOrEmpty(password))
            {
                return BadRequest("User data and password are required.");
            }

            try
            {
                var user1 = _db.user.FirstOrDefault(a => a.username == Username);
                

                if (user1 != null)
                {
                    // Verify the password using BCrypt
                    if (BCrypt.Net.BCrypt.Verify(password, user1.Password))
                    {
                       /* var b = _db.user.First(u => u.username == Username);

                        var pay = _db.usingpack.Where(u => u.RecrComp == b.RecrComp).FirstOrDefault();
                        // Perform login logic (you might want to return tokens, etc. in an API)
                        if (pay == null)
                        {

                            return BadRequest("Company doesn't have any payment");
                        }
                        else
                        {
                            if (pay.Exp_Day.AddYears(1) == DateTime.Now)
                            {
                                if (0 < b.Nr_Employ && b.Nr_Employ <= 20)
                                {
                                    PaymentController.Price = 30000;
                                }
                                else if (20 < b.Nr_Employ && b.Nr_Employ <= 50)
                                {
                                    PaymentController.Price = 50000;
                                }
                                else if (50 < b.Nr_Employ)
                                {
                                    PaymentController.Price = 70000;
                                }

                            }


                            else if (pay.Exp_Day.AddYears(1) <= DateTime.Now)
                            {
                                if (0 < b.Nr_Employ && b.Nr_Employ <= 20)
                                {
                                    PaymentController.Price = 30000;
                                }
                                else if (20 < b.Nr_Employ && b.Nr_Employ <= 50)
                                {
                                    PaymentController.Price = 50000;
                                }
                                else if (50 < b.Nr_Employ)
                                {
                                    PaymentController.Price = 70000;
                                }

                            }
*/

                           /* else if (pay.Exp_Day.AddYears(1) >= DateTime.Now)
                            {*/
                                    

                                    EmployController.CompanyName = user1.username;
                                    ExportController.CompName = user1.username;
                                PasswordController.username = user1.username;
                                ApplicantsController.Username = user1.username;
                                CompanyController.RecruteComp = user1.username;

                            return Ok(user1.Role);
                        
                  }
                    else
                    {
                        return Unauthorized("Invalid User");
                    }
                            }
                        
                                      
                
                else
                {
                    return NotFound("Invalid username");
                }
               
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"Error during login: {ex.Message}");
                return StatusCode(500, "An error occurred during login.");
            }
        }

    }
}
