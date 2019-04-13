using Dealership.Data;
using Dealership.Data.Interface;
using Dealership.Models;
using Dealership.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dealership.Web.Controllers
{
    [Authorize(Roles ="sales")]
    public class SalesController : Controller
    {
        IDealerRepo _carDealer = CarDealerFactory.Create();

        // GET: Sales
        public ActionResult Index()
        {
            var model = _carDealer.GetAvailableVehicles();
            return View(model);
        }

        public ActionResult Purchase(int id)
        {
            var vehicle = _carDealer.GetVehicleById(id);
            var model = _carDealer.GetAllPurchaseTypes();
            PurchaseViewModel pvm = new PurchaseViewModel();
            pvm.SetPurchaseTypes(model);
            return View(pvm);
        }

        [HttpPost]
        public ActionResult Purchase(PurchaseViewModel model)
        {
            _carDealer.AddPurchase(model);
            return RedirectToAction("Index");
        }
    }
}