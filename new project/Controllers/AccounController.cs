using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Security.Claims;
using new_project.Models;
using Microsoft.AspNetCore.Authorization;

namespace new_project.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            ViewData["error"] = "";
            return View();
        }

        [HttpPost]
        public IActionResult Login(userlogin model)
        {
            //check from db
            string connString = "server = SAAHID; database = SAAHID3; integrated security = true; TrustServerCertificate = True";
            using (SqlConnection con = new SqlConnection(connString))
            {
                con.Open();
                string query = $"select count(*) total from users where username='{model.username}' and password='{model.password}'";
                SqlCommand cmd = new SqlCommand(query, con);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count > 0)
                {
                    //user is valid
                    //create session
                    HttpContext.Session.SetString("username", model.username);

                    //give auth cookie
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, model.username),
                        new Claim(ClaimTypes.Role,"Admin")

                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var princible = new ClaimsPrincipal(identity);
                    HttpContext.SignInAsync(princible);




                    return RedirectToAction("Create", "Teacher");
                }
                else
                {
                    //user is invalid
                    ViewData["error"] = "Invalid Credentials";
                    return View(model);
                }


            }

        }

        public IActionResult logout()
        {
            HttpContext.Session.Remove("username");
            HttpContext.SignOutAsync();
            return RedirectToAction("login");
        }
    }
}


