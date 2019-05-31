using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lab22.Models;

namespace Lab22
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Shop()
        {

            ShopDBEntities shopDB = new ShopDBEntities();

            ViewBag.Items = shopDB.Items.ToList();

            return View();
        }

        [HttpPost]
        public ActionResult Shop(string search)
        {

            ShopDBEntities shopDB = new ShopDBEntities();

            ViewBag.Items = shopDB.Items.Where(x => x.Name.Contains(search)).ToList();

            return View();
        }


        public ActionResult MakePurchase(string name)
        {
            ShopDBEntities shopDB = new ShopDBEntities();
            Item item = shopDB.Items.FirstOrDefault(x => x.Name == name);            

            if(item != null)
            {
                User user = (User)Session["CurrentUser"];
                User userFromDb = shopDB.Users.FirstOrDefault(u => u.UserName == user.UserName);

                if (userFromDb.Funds != null)
                {
                    if (item.Price > userFromDb.Funds)
                    {
                        ViewBag.MessageTitle = "Insufficient Funds";
                        ViewBag.Message = $"This item cost {item.Price:C2}. You have {userFromDb.Funds:C2}. " +
                            $"You will need {(item.Price - userFromDb.Funds):C2}";
                        return View("Error");
                    }

                    userFromDb.Funds -= item.Price;
                    userFromDb.ConfirmPassword = userFromDb.Password;                    
                    item.Quantity -= 1;

                    try
                    {                        
                        shopDB.SaveChanges();
                        ViewBag.Message = $"Thanks for puchasing {item.Name}.";
                    }
                    catch(Exception e)
                    {
                        ViewBag.Message = e.Message;
                        return View("Error");
                    }
                }
                else
                {
                    ViewBag.MessageTitle = "Insufficient Funds";
                    ViewBag.Message = $"This item cost {item.Price:C2}. You have $0.00. You will need {item.Price.ToString("0.00")}";
                    return View("Error");
                }
            }

            ViewBag.Items = shopDB.Items.ToList();
            return View("Shop");
        }
    }
}