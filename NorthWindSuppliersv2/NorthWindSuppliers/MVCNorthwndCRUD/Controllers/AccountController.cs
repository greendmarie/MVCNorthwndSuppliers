using DataLayer;
using DataLayer.Models;
using MVCNorthwndCRUD.Mapping;
using MVCNorthwndCRUD.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCNorthwndCRUD.Controllers
{
    public class AccountController : Controller
    {
        private UserDAO dataAccess;
        public AccountController()
        {
            string filePath = ConfigurationManager.AppSettings["logPath"];
            dataAccess = new UserDAO(connectionString, filePath);
        }

        private static string connectionString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

        // GET: Account
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginPO form)
        {
            ActionResult oResponse = RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {
                try
                {
                    UserDO storedUser = dataAccess.ReadUserByUsername(form.Username);
                    if (storedUser != null && form.Password.Equals(storedUser.Password))
                    {
                        Session["RoleID"] = storedUser.RoleID;
                        Session["Username"] = storedUser.Username;
                        Session["UserID"] = storedUser.UserID;
                        Session.Timeout = 3;
                    }
                }
                catch(Exception ex)
                {
                    //Erroe message
                }
            }
            else
            {
                ModelState.AddModelError("Password", "Username or Password is incorrect");
            }

            return oResponse;
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserPO form)
        {
            //Declaring local variables
            ActionResult oResponse = RedirectToAction("Login", "Account");

            if (ModelState.IsValid)
            {
                try
                {
                    form.RoleID = 3;
                    UserDO dataObject = UserMapper.MapPoToDo(form);
                    dataAccess.CreateUser(dataObject);

                    TempData["Message"] = "New Account Created";
                }
                catch (Exception ex)
                {
                    oResponse = View(form);
                }
            }
            else
            {
                oResponse = View(form);
            }

            return oResponse;
        }

        [HttpGet]
        public ActionResult Logout()
        {



            return RedirectToAction("Index", "Home");
        }



    }
}