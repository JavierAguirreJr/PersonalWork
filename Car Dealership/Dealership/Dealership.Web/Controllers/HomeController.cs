using Dealership.Data;
using Dealership.Data.Interface;
using Dealership.Models;
using Dealership.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dealership.Web.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        IDealerRepo _carDealer = CarDealerFactory.Create();

        // GET: Home
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        { 
            var model = _carDealer.GetFeaturedVehicles();

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult NewVehicles()
        {
            VehicleSearchInfo info = new VehicleSearchInfo();

            var model = _carDealer.GetNewVehicles(info);

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult UsedVehicles()
        {
            //VehicleSearchInfo info = new VehicleSearchInfo();

            //var model = _carDealer.GetUsedVehicles(info);

            return View(/*model*/);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Specials()
        {
            var model = _carDealer.GetSpecials();

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ContactUs()
        {
            return View();
        }













        //admin login process

        [AllowAnonymous]
        public ActionResult Login()
        {
            var model = new LoginViewModel();

            return View(model);
        }

        

        //updating UI to allow login
        //milestone 4 lesson 11
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
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

        public ActionResult Signout()
        {
            var logout = Request.GetOwinContext();
            var auth = logout.Authentication;
            auth.SignOut("ApplicationCookie");

            return RedirectToAction("Index", "Home");
        }
    }
}