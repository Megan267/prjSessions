using Microsoft.AspNetCore.Mvc;

namespace prjSessions.Controllers
{
    public class DashboardController : Controller
    {
        //This is the protected page
        public IActionResult Index()
        {
            // --- SESSION CHECK ---
            // Retrive the Username from the session
            string username = HttpContext.Session.GetString("Username");

            //If the username is null or empty, the user is not logged in
            if(string.IsNullOrEmpty(username))
            {
                //Redirect them to the login action in the Account controller
                return RedirectToAction("Index", "Account");
            }

            //If the session exists, pass the username to the view and show the page.
            ViewBag.Username = username;
            return View();
        }
    }
}
