using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using new_project.Models;

namespace new_project.Controllers
{

    public class AccountsController : Controller
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
            string connString = "server=SAAHID; database=SAAHID3; integrated security=true; TrustServerCertificate=True";
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
                    return RedirectToAction("index", "Home");
                }
                else
                {
                    //user is invalid
                    ViewData["error"] = "Invalid Credentials";
                    return View(model);
                }


            }

        }
    }

}
