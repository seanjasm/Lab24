using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Lab22.Models;

namespace Lab22.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult MakeNewUser(User u)
        {
            if (ModelState.IsValid)
            {
                
                ShopDBEntities shopDB = new ShopDBEntities();
                try
                {
                    shopDB.Users.Add(u);
                    shopDB.SaveChangesAsync();
                    Session["CurrentUser"] = u;
                    Session["UserName"] = u.FirstName;
                    ViewBag.Message = $"{u.FirstName} was added successfully to database";
                }
                catch(Exception)
                {
                    ViewBag.Message = "There was a problem adding new user. Please see admin for help.";
                }

                return View();
            }

            return RedirectToAction("Register");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string UserName, string Password)
        {
            if (UserName != "" && Password != "")
            {
                ShopDBEntities shopDB = new ShopDBEntities();
                User user = shopDB.Users.Where(x => x.UserName == UserName).
                    Where(y => y.Password == Password).First();
                
                if (user != null)
                {
                    Session["CurrentUser"] = user;
                    Session["UserName"] = user.FirstName;
                    return View("../Home/Index");
                }
            }

            return View("Login");
        }
    }
}