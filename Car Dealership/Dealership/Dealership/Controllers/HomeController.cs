using Dealership.Data;
using Dealership.Data.Interface;
using Dealership.Models;
using Dealership.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;

namespace Dealership.Controllers
{
    public class HomeController : Controller
    {
        IDealerRepo _carDealer;

        public HomeController()
        {
            //_carDealer = CarDealerFactory.Create(ConfigurationManager.AppSettings["Mode"].ToString());
        }


        [AllowAnonymous]
        public ActionResult Login()
        {
            var model = new LoginViewModel();

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login (LoginViewModel model, string returnUrl)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            //GetOwinContext requires nuget package
            //Microsoft.Owin.Host.SystemWeb
            var userManager = HttpContext.GetOwinContext().GetUserManager<UserManager<AppUser>>();
            var authManager = HttpContext.GetOwinContext().Authentication;

            AppUser user = userManager.Find(model.UserName, model.Password);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid username or password");

                return View(model);
            }

            else
            {
                var identity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                authManager.SignIn(new AuthenticationProperties { IsPersistent = model.RememberMe }, identity);

                if (!string.IsNullOrEmpty(returnUrl))
                    return Redirect(returnUrl);

                else
                    return RedirectToAction("Index");
            }
        }

        //continue from updating UI to allow login
        //milestone 4 lesson 11

        // GET: Home
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            var model = _carDealer.GetFeaturedVehicles();

            return View(model);
        }


    }
}