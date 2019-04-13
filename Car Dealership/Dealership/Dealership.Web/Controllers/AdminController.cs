using Dealership.Data;
using Dealership.Data.Interface;
using Dealership.Models;
using Dealership.Models.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dealership.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        IDealerRepo _carDealer = CarDealerFactory.Create();
        CarDealershipDbContext context = new CarDealershipDbContext();

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Vehicles()
        {
            return View();
        }

        //authorize section on milstone 4 lesson 11
        //[Authorize(Roles = "admin")]
        public ActionResult AddVehicle()
        {
            var model = new AddVehicleViewModel();
            model.SetMakeType(_carDealer.GetAllCarMakes());
            model.SetModelType(_carDealer.GetAllCarModels());
            model.SetBodyType(_carDealer.GetBodyStyles());
            model.SetVehicleType(_carDealer.GetVehicleType());
            model.SetTransmissionType(_carDealer.GetTransmissions());
            model.SetColorOptions();
            model.SetInteriorTypes();
            //throw new Exception();

            return View(model);
        }

        [HttpPost]
        public ActionResult AddVehicle(AddVehicleViewModel model)
        {
            VehicleDetails vehicleAdded = new VehicleDetails();
            var localhost = "http://localhost:50822/Images/";

            if (string.IsNullOrEmpty(model.Vehicle.Description))
            {
                ModelState.AddModelError("Description", "Vehicle must have some sort of description.");
            }
            if (/*string.IsNullOrEmpty(localhost) &&*/ model.VehicleImage == null)
            {
                ModelState.AddModelError("VehicleImage", "Image required for vehicle!");
            }

            if (model.Vehicle.VIN == null || model.Vehicle.VIN.Length != 17)
            {
                ModelState.AddModelError("VIN", "Vehicle VIN numbers must be 17 digits long!");
            }

            if (model.Vehicle.MSRP < vehicleAdded.SalePrice)
            {
                ModelState.AddModelError("SalePrice", "Sale Price can not be larger than MSRP!");
            }

            if (model.VehicleImage != null && model.VehicleImage.ContentLength > 0)
            {
                var savePath = Server.MapPath("~/Images");

                string fileName = Path.GetFileName(model.VehicleImage.FileName);
                string extension = Path.GetExtension(model.VehicleImage.FileName);

                var filePath = Path.Combine(savePath, fileName + extension);

                int counter = 1;
                while (System.IO.File.Exists(filePath))
                {
                    filePath = Path.Combine(savePath, fileName + counter.ToString() + extension);
                    counter++;
                }

                model.VehicleImage.SaveAs(filePath);
            }


            if (ModelState.IsValid)
            {
                //VehicleDetails vehicleAdded = new VehicleDetails();
                vehicleAdded.Color = model.Vehicle.Color;
                vehicleAdded.Interior = model.Vehicle.Interior;
                vehicleAdded.BodyName = model.Vehicle.BodyName;
                vehicleAdded.Description = model.Vehicle.Description;
                vehicleAdded.Mileage = model.Vehicle.Mileage;
                vehicleAdded.ModelName = model.Vehicle.ModelName;
                vehicleAdded.TransmissionType = model.Vehicle.TransmissionType;
                vehicleAdded.VehicleType = model.Vehicle.VehicleType;
                vehicleAdded.VIN = model.Vehicle.VIN;
                vehicleAdded.Year = model.Vehicle.Year;
                vehicleAdded.MSRP = model.Vehicle.MSRP;
                vehicleAdded.SalePrice = model.Vehicle.SalePrice;
                vehicleAdded.ModelName.MakeName.MakeID = model.Vehicle.ModelName.MakeName.MakeID;
                vehicleAdded.ImageUrl = localhost + model.VehicleImage.FileName;

                _carDealer.CreateVehicle(model);
            }

            else
            {
                model.SetMakeType(_carDealer.GetAllCarMakes());
                model.SetModelType(_carDealer.GetAllCarModels());
                model.SetBodyType(_carDealer.GetBodyStyles());
                model.SetVehicleType(_carDealer.GetVehicleType());
                model.SetTransmissionType(_carDealer.GetTransmissions());
                model.SetColorOptions();
                model.SetInteriorTypes();

                return View(model);
            }
            return RedirectToAction("Vehicles");
        }

        public ActionResult EditVehicle(int id)
        {
            var model = new EditVehicleViewModel();
            model.Vehicle = _carDealer.GetVehicleById(id);

            model.SetMakeType(_carDealer.GetAllCarMakes());
            model.SetModelType(_carDealer.GetAllCarModels());
            model.SetBodyType(_carDealer.GetBodyStyles());
            model.SetVehicleType(_carDealer.GetVehicleType());
            model.SetTransmissionType(_carDealer.GetTransmissions());
            model.SetColorOptions();
            model.SetInteriorTypes();
            //throw new Exception();

            return View(model);
        }

        [HttpPost]
        public ActionResult EditVehicle(EditVehicleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var localhost = "http://localhost:50822/Images/";

                //if (string.IsNullOrEmpty(model.Vehicle.Image) && model.VehicleImage == null)
                //{
                //    ModelState.AddModelError("VehicleImage", "Image required for vehicle!");
                //}
                if(string.IsNullOrEmpty(model.Vehicle.Description))
                {
                    ModelState.AddModelError("Description", "Vehicle must have some sort of description.");
                }
                if (model.Vehicle.VIN == null || model.Vehicle.VIN.Length != 17)
                {
                    ModelState.AddModelError("VIN", "Vehicle VIN numbers must be 17 digits long!");
                }

                if (model.Vehicle.MSRP < model.Vehicle.SalePrice)
                {
                    ModelState.AddModelError("SalePrice", "Sale Price can not be larger than MSRP!");
                }

                if (model.VehicleImage != null && model.VehicleImage.ContentLength > 0)
                {
                    var savePath = Server.MapPath("~/Images");

                    string fileName = Path.GetFileName(model.VehicleImage.FileName);
                    string extension = Path.GetExtension(model.VehicleImage.FileName);

                    var filePath = Path.Combine(savePath, fileName + extension);

                    model.VehicleImage.SaveAs(filePath);
                    model.Vehicle.ImageUrl = localhost + model.VehicleImage.FileName;

                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                        model.VehicleImage.SaveAs(filePath);
                    }

                    //int counter = 1;
                    //while (System.IO.File.Exists(filePath))
                    //{
                    //    filePath = Path.Combine(savePath, fileName + counter.ToString() + extension);
                    //    counter++;
                    //}
                }
                _carDealer.EditVehicle(model);
            }

            else
            {
                model.SetMakeType(_carDealer.GetAllCarMakes());
                model.SetModelType(_carDealer.GetAllCarModels());
                model.SetBodyType(_carDealer.GetBodyStyles());
                model.SetVehicleType(_carDealer.GetVehicleType());
                model.SetTransmissionType(_carDealer.GetTransmissions());
                model.SetColorOptions();
                model.SetInteriorTypes();

                return View(model);
            }

            return RedirectToAction("Vehicles");
        }

        public ActionResult DeleteVehicle(int id)
        {
            var model = _carDealer.GetVehicleById(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult DeleteVehicle(VehicleDetails vehicle)
        {
            var toDelete = _carDealer.GetVehicleById(vehicle.VehicleId);
            _carDealer.Deletevehicle(toDelete.VehicleId);
            return RedirectToAction("Vehicles");
        }

        public ActionResult Users()
        {
            var model = _carDealer.GetAllUsers();
            return View(model);
        }

        public ActionResult AddUser()
        {
            //var user = _carDealer.CreateUser(model);
            var model = new UserRoleViewModel();
            model.SetRoleItems(_carDealer.GetAllRoles());

            return View(model);
            //throw new Exception();
        }

        [HttpPost]
        public ActionResult AddUser(UserRoleViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.AppUser.FirstName))
            {
                ModelState.AddModelError("FirstName", "Please enter user first name");
            }
            if (string.IsNullOrWhiteSpace(model.AppUser.LastName))
            {
                ModelState.AddModelError("LastName", "Please enter user last name");
            }
            if (string.IsNullOrWhiteSpace(model.AppUser.Email))
            {
                ModelState.AddModelError("Email", "Please enter the email for the user");
            }
            if (string.IsNullOrEmpty(model.RoleId))
            {
                ModelState.AddModelError("Role", "Please select a role for the user.");
            }
            if (string.IsNullOrWhiteSpace(model.Password) || string.IsNullOrWhiteSpace(model.ConfirmPassword))
            {
                ModelState.AddModelError("Password", "Please enter a password");
            }
            if (model.Password.Length < 6)
            {
                ModelState.AddModelError("Password", "Password length must be at least 6 characters long");
            }
            if (model.ConfirmPassword != model.Password)
            {
                ModelState.AddModelError("ConfirmPassword", "Must have mmtching passwords");
            }

            if (ModelState.IsValid)
            {
                _carDealer.CreateUser(model);
                return RedirectToAction("Users");
            }
            else
            {
                model.SetRoleItems(_carDealer.GetAllRoles());
                return View(model);
            }

        }

        public ActionResult EditUser(string id)
        {
            //var user = new UserRoleViewModel();
            //user.SetRoleItems(_carDealer.GetAllRoles());
            //user.AppUser = _carDealer.GetUserById(id);

            var user = _carDealer.GetUserById(id);
            //simplified by VS: O
            var model = new UserRoleViewModel
            {
                AppUser = user,
                RoleId = user.Roles.Single().RoleId
            };

            model.SetRoleItems(_carDealer.GetAllRoles());

            return View(model);
        }

        [HttpPost]
        public ActionResult EditUser(UserRoleViewModel model)
        {
            var userMgr = new UserManager<AppUser>(new UserStore<AppUser>(context));
            var roleMgr = new RoleManager<AppRole>(new RoleStore<AppRole>(context));
            var findUser = userMgr.FindById(model.AppUser.Id);

            var user = userMgr.FindById(findUser.Id);
            var userRoles = userMgr.GetRoles(user.Id);

            if (string.IsNullOrWhiteSpace(model.AppUser.FirstName))
            {
                ModelState.AddModelError("FirstName", "Please enter user first name");
            }
            if (string.IsNullOrWhiteSpace(model.AppUser.LastName))
            {
                ModelState.AddModelError("LastName", "Please enter user last name");
            }
            if (string.IsNullOrWhiteSpace(model.AppUser.Email))
            {
                ModelState.AddModelError("Email", "Please enter the email for the user");
            }
            if (string.IsNullOrEmpty(model.RoleId))
            {
                ModelState.AddModelError("Role", "Please select a role for the user.");
            }
            //if (string.IsNullOrWhiteSpace(model.Password) || string.IsNullOrWhiteSpace(model.ConfirmPassword))
            //{
            //    ModelState.AddModelError("Password", "Please enter a password");
            //}
            if(!string.IsNullOrEmpty(model.Password) && model.Password.Length<6)
            {
                ModelState.AddModelError("Password", "Password length must be at least 6 characters long");
            }
            if (model.ConfirmPassword != model.Password)
            {
                ModelState.AddModelError("ConfirmPassword", "Must have mmtching passwords");
            }

            if (ModelState.IsValid)
            {
                userMgr.RemoveFromRoles(user.Id, userRoles.ToArray());
                var matchingRole = roleMgr.Roles.Single(r => r.Id == model.RoleId);
                userMgr.AddToRole(user.Id, matchingRole.Name);

                _carDealer.EditUser(model);

                user.FirstName = model.AppUser.FirstName;
                user.LastName = model.AppUser.LastName;
                user.UserName = model.AppUser.UserName;
                user.Id = model.AppUser.Id;
                user.Email = model.AppUser.Email;

                userMgr.Update(user);

                return RedirectToAction("Users");
            }
            else
            {
                model.SetRoleItems(_carDealer.GetAllRoles());
                return View(model);
            }

        }

        [Authorize(Roles = "admin,sales")]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(UserRoleViewModel password)
        {
            var userId = User.Identity.GetUserId();
            var user = _carDealer.GetUserById(userId);
            user = password.AppUser;

            if (password.ConfirmPassword != password.Password)
            {
                ModelState.AddModelError("ConfirmPassword", "Passwords must match to be saved!");
            }

            if (ModelState.IsValid)
            {
                _carDealer.UpdatePassword(password);
                return View();
            }
            else
            {
                return View();
            }
        }

        public ActionResult Makes()
        {
            var model = new AddMakeViewModel();
            model.MakeList = _carDealer.GetAllCarMakes();
            return View(model);
        }

        public ActionResult AddMake()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddMake(CarMake addMake)
        {
            var userId = User.Identity.GetUserId();
            var user = _carDealer.GetUserById(userId);
            addMake.User = user;
            addMake.DateAdded = DateTime.Now;

            _carDealer.CreateMake(addMake);
            return RedirectToAction("Makes");
        }

        public ActionResult Models()
        {
            var models = new AddModelViewModel();
            models.ModelList = _carDealer.GetAllCarModels();
            return View(models);
        }

        public ActionResult AddModels()
        {

            var makes = _carDealer.GetAllCarMakes();
            return View(makes);
        }

        [HttpPost]
        public ActionResult AddModels(int makeId, string modelName)
        {
            var userId = User.Identity.GetUserId();
            var user = _carDealer.GetUserById(userId);
            CarModel newModel = new CarModel();

            newModel.Model = modelName;
            newModel.User = user;
            newModel.DateAdded = DateTime.Now;
            newModel.MakeName = _carDealer.GetMakeById(makeId);

            _carDealer.CreateModel(newModel);
            return RedirectToAction("Models");
        }

        public ActionResult Specials()
        {
            var specials = _carDealer.GetSpecials();
            return View(specials);
        }

        public ActionResult AddSpecial()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddSpecials(Specials addSpecial)
        {
            if (ModelState.IsValid)
            {
                _carDealer.CreateSpecial(addSpecial);
                return RedirectToAction("Special");
            }
            else
            {
                return View();
            }

        }

        public ActionResult DeleteSpecial(int id)
        {
            var toDelete = _carDealer.GetSpecialsById(id);
            return View(toDelete);
        }

        [HttpPost]
        public ActionResult DeleteSpecial(Specials deleteSpecial)
        {
            var delete = _carDealer.GetSpecialsById(deleteSpecial.SpecialsId);
            _carDealer.DeleteSpecial(delete);
            return RedirectToAction("Special");
        }
    }
}