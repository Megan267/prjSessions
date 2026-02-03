using Microsoft.AspNetCore.Mvc;

namespace prjSessions.Controllers
{
    public class AccountController : Controller
    {
        //GET: /Account/Login
        //Displays the login form to the user
        public IActionResult Index()
        {
            return View();
        }

        //POST: /Account/Login
        //This action is triggered when the user submits the login form
        [HttpPost]
        public IActionResult Index(string username, string password)
        {
            //In a real application, you would validate the username and password against a database
            //For this example, we'll just check for a hardcded user
            if (!string.IsNullOrEmpty(username) && password == "password123")
            {
                //--- SESSION CREATION ---
                //If credentials are valid, create a session variable
                //to store the username
                //This marks the user as "logged in"
                HttpContext.Session.SetString("Username", username);
                //Redirect tthe user to the protected dashboard page
                return RedirectToAction("Dashboard", "Home");
            }
            else
            {
                //If login fails, show an error messsage and display the login form again
                ViewBag.ErrorMessage = "Invalid username or password.";
                return View();
            }
        }

        //Get: /Account/Logout
        //This action logs the user out
        public IActionResult Logout()
        {
            //--- SESSION DESTRUCTION ---
            //Clear the session variable to log the user out
            HttpContext.Session.Clear();
            //Redirect the user back to the home page
            return RedirectToAction("Index", "Home");
        }
    }
}
