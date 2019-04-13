using Dealership.Data;
using Dealership.Data.Interface;
using Dealership.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Dealership.Web.Controllers
{
    [EnableCors(origins:"*",headers:"*",methods:"*")]
    public class VehicleController : ApiController
    {
        IDealerRepo repo = CarDealerFactory.Create();

        [Route("Inventory/New/{search}/{minPrice}/{maxPrice}/{minYear}/{maxYear}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetNew
            (string search, int minPrice, int maxPrice, int minYear, int maxYear)
        {
            VehicleSearchInfo info = new VehicleSearchInfo()
            {
                SearchBoxInfo = search,
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                MinYear = minYear,
                MaxYear = maxYear,
                IsNew = true
            };

            return Ok(repo.GetNewVehicles(info));
        }

        [Route("Inventory/Used/{search}/{minPrice}/{maxPrice}/{minYear}/{maxYear}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetUsed
            (string search, int minPrice, int maxPrice, int minYear, int maxYear)
        {
            VehicleSearchInfo info = new VehicleSearchInfo()
            {
                SearchBoxInfo = search,
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                MinYear = minYear,
                MaxYear = maxYear,
                IsNew = false
            };

            return Ok(repo.GetUsedVehicles(info));
        }

        [Route("Inventory/Details/{id}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult Get(int id)
        {
            VehicleDetails found = repo.GetVehicleById(id);

            if(found == null)
            {
                return NotFound();
            }

            return Ok(found);
        }

        [Route("Home/featured")]
        public List<VehicleDetails> GetFeatured()
        {
            var model = repo.GetFeaturedVehicles();

            return model;
        }

        [Route("Admin/Vehicles/{search}/{minPrice}/{maxPrice}/{minYear}/{maxYear}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult AdminSearchVehicles
            (string search, int minPrice, int maxPrice, int minYear, int maxYear)
        {
            VehicleSearchInfo info = new VehicleSearchInfo()
            {
                SearchBoxInfo = search,
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                MinYear = minYear,
                MaxYear = maxYear,
            };

            return Ok(repo.GetAllVehicles(info));
        }

        //[Route("Admin/EditVehicle/{id}")]
        //[AcceptVerbs("GET")]
        //public IHttpActionResult AdminEditVehicles(int id)
        //{
        //    VehicleDetails found = repo.GetVehicleById(id);

        //    if (found == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(found);
        //}
    }
}
