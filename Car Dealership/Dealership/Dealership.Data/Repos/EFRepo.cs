using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Dealership.Data.Interface;
using Dealership.Models;
using Dealership.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System.Data.Entity.Migrations;
using System.Web;
using System.IO;

namespace Dealership.Data.Repos
{
    public class EFRepo : IDealerRepo
    {
        CarDealershipDbContext dealerDb = new CarDealershipDbContext();

        public void AddPurchase(PurchaseViewModel purchase)
        {
            Sale carPurchase = purchase.SaleDetails;
            carPurchase.DateSold = DateTime.Now;
            carPurchase.User = purchase.SaleDetails.User;
            carPurchase.PurchaseType = dealerDb.Purchase.SingleOrDefault(p => p.PurchaseTypeId == purchase.PurchaseTypeDetails.PurchaseTypeId);

            VehicleDetails vehicleSold = dealerDb.VehicleDetails.SingleOrDefault(v => v.VehicleId == purchase.VehiclePurchaseId);
            carPurchase.IsSold = true;
            carPurchase.IsFeatured = false;
            carPurchase.Vehicle = vehicleSold;
            carPurchase.VehicleId = vehicleSold.VehicleId;

            dealerDb.SaveChanges();
            dealerDb.Sale.Add(carPurchase);
            dealerDb.SaveChanges();
        }

        public void CreateMake(CarMake newMake)
        {
            newMake.DateAdded = DateTime.Now;
            newMake.User = dealerDb.Users.SingleOrDefault(m => m.Id == newMake.User.Id);

            dealerDb.CarMake.Add(newMake);
            dealerDb.SaveChanges();
        }

        public void CreateModel(CarModel newModel)
        {
            AddVehicleViewModel addModel = new AddVehicleViewModel();

            if (dealerDb.CarModel.Count() == 0)
            {
                newModel.ModelID = 1;
            }
            else
            {
                newModel.ModelID = dealerDb.VehicleDetails.Max(m => m.ModelName.ModelID) + 1;
                newModel.DateAdded = DateTime.Now;
                newModel.User = dealerDb.Users.SingleOrDefault(u => u.Id == newModel.User.Id);
                addModel.SetMakeType(GetAllCarMakes());
            }
            //newModel = addModel.Vehicle.ModelName;
            //newModel.MakeName = dealerDb.CarMake.FirstOrDefault(m => m.MakeID == addModel.Vehicle.ModelName.MakeName.MakeID);

            dealerDb.CarModel.Add(newModel);
            dealerDb.SaveChanges();
        }

        public void CreateSpecial(Specials newSpecial)
        {
            throw new NotImplementedException();
        }

        public void CreateUser(UserRoleViewModel viewModel)
        {
            using (CarDealershipDbContext context = new CarDealershipDbContext())
            {
                viewModel.AppUser.UserName = viewModel.AppUser.Email;

                var userMgr = new UserManager<AppUser>(new UserStore<AppUser>(context));
                var roleMgr = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

                if (userMgr.FindByName(viewModel.AppUser.UserName) == null)
                {
                    var newuser = new AppUser()
                    {
                        FirstName = viewModel.AppUser.FirstName,
                        LastName = viewModel.AppUser.LastName,
                        UserName = viewModel.AppUser.UserName,
                        Email = viewModel.AppUser.Email
                    };
                    userMgr.Create(newuser, viewModel.Password);
                    dealerDb.SaveChanges();
                }

                var user = userMgr.FindByName(viewModel.AppUser.UserName);
                var role = dealerDb.Roles.SingleOrDefault(r => r.Id == viewModel.RoleId);

                userMgr.AddToRole(user.Id, role.Name);
                dealerDb.SaveChanges();
            }

        }

        public void CreateVehicle(AddVehicleViewModel newVehicle)
        {
            VehicleDetails addVehicle = new VehicleDetails();

            addVehicle = newVehicle.Vehicle;
            addVehicle.VehicleId = newVehicle.Vehicle.VehicleId;
            addVehicle.ModelName = dealerDb.CarModel.FirstOrDefault(m => m.ModelID == newVehicle.Vehicle.ModelName.ModelID);
            addVehicle.BodyName = dealerDb.BodyStyle.FirstOrDefault(b => b.BodyStyleId == newVehicle.Vehicle.BodyName.BodyStyleId);
            addVehicle.Color = newVehicle.Vehicle.Color;
            addVehicle.TransmissionType = dealerDb.Transmission.FirstOrDefault(t => t.TransmissionId == newVehicle.Vehicle.TransmissionType.TransmissionId);
            addVehicle.VehicleType = dealerDb.VehicleType.FirstOrDefault(y => y.VehicleTypeId == newVehicle.Vehicle.VehicleType.VehicleTypeId);
            addVehicle.ImageUrl = newVehicle.Vehicle.ImageUrl;
            addVehicle.IsSold = false;
            addVehicle.ImageUrl = newVehicle.Vehicle.ImageUrl;
            dealerDb.VehicleDetails.Add(addVehicle);
            dealerDb.SaveChanges();
        }

        public bool DeleteMake(int deleteMake)
        {
            throw new NotImplementedException();
        }

        public bool DeleteModel(int deleteModel)
        {
            throw new NotImplementedException();
        }

        public void DeleteSpecial(Specials deleteSpecial)
        {
            throw new NotImplementedException();
        }

        public void Deletevehicle(int deleteVehicle)
        {
            var toDelete = (from v in dealerDb.VehicleDetails
                            where v.VehicleId == deleteVehicle
                            select v).FirstOrDefault();

            dealerDb.VehicleDetails.Remove(toDelete);
            dealerDb.SaveChanges();
        }

        public void EditMake(CarMake editMake)
        {
            throw new NotImplementedException();
        }

        public void EditModel(CarModel editModel)
        {
            throw new NotImplementedException();
        }

        public void EditSpecial(Specials editSpecial)
        {
            throw new NotImplementedException();
        }

        public void EditUser(UserRoleViewModel viewModel)
        {
            //seed file concept, M4 L11
            viewModel.AppUser.UserName = viewModel.AppUser.Email;
            var store = new UserStore<AppUser>(dealerDb);
            var userMgr = new UserManager<AppUser>(new UserStore<AppUser>(dealerDb));
            var roleMgr = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(dealerDb));

            AppUser update = userMgr.FindById(viewModel.AppUser.Id);
            update.FirstName = viewModel.AppUser.FirstName;
            update.LastName = viewModel.AppUser.LastName;
            update.Email = viewModel.AppUser.Email;

            if (!string.IsNullOrEmpty(viewModel.Password) && !string.IsNullOrEmpty(viewModel.ConfirmPassword))
            {
                update.PasswordHash = userMgr.PasswordHasher.HashPassword(viewModel.ConfirmPassword);
            }

            userMgr.RemoveFromRoles(update.Id, userMgr.GetRoles(update.Id).ToArray());
            userMgr.Update(update);

            var user = userMgr.FindById(viewModel.AppUser.Id);
            var role = roleMgr.FindById(viewModel.RoleId);

            userMgr.AddToRole(user.Id, role.Name);
            dealerDb.SaveChanges();
        }

        public void EditVehicle(EditVehicleViewModel editVehicle)
        {
            using (var newDb = new CarDealershipDbContext())
            {
                //admin controller setup, follow assining of properties
                VehicleDetails toEdit = newDb.VehicleDetails.FirstOrDefault(v => v.VehicleId == editVehicle.Vehicle.VehicleId);

                //toEdit = editVehicle.Vehicle;
                //toEdit.VehicleId = editVehicle.Vehicle.VehicleId;
                toEdit.ModelName = newDb.CarModel.FirstOrDefault(m => m.ModelID == editVehicle.Vehicle.ModelName.ModelID);
                //toEdit.ModelName.ModelID = toEdit.ModelName.ModelID;

                toEdit.BodyName = newDb.BodyStyle.FirstOrDefault(b => b.BodyStyleId == editVehicle.Vehicle.BodyName.BodyStyleId);

                toEdit.Color = editVehicle.Vehicle.Color;
                toEdit.Interior = editVehicle.Vehicle.Interior;
                toEdit.TransmissionType = newDb.Transmission.FirstOrDefault(t => t.TransmissionId == editVehicle.Vehicle.TransmissionType.TransmissionId);
                toEdit.MSRP = editVehicle.Vehicle.MSRP;
                toEdit.SalePrice = editVehicle.Vehicle.SalePrice;

                toEdit.Description = editVehicle.Vehicle.Description;
                toEdit.VIN = editVehicle.Vehicle.VIN;
                toEdit.Mileage = editVehicle.Vehicle.Mileage;
                toEdit.IsFeatured = editVehicle.Vehicle.IsFeatured;

                toEdit.VehicleType = newDb.VehicleType.FirstOrDefault(y => y.VehicleTypeId == editVehicle.Vehicle.VehicleType.VehicleTypeId);

                if (editVehicle.VehicleImage != null)
                {
                    toEdit.ImageUrl = editVehicle.Vehicle.ImageUrl;
                }
                toEdit.IsSold = false;

                newDb.VehicleDetails.AddOrUpdate(toEdit);
                newDb.SaveChanges();
            }

        }

        public List<CarMake> GetAllCarMakes()
        {
            var allMakes = (from m in dealerDb.CarMake
                            select m).ToList();

            return allMakes;
        }

        public List<CarModel> GetAllCarModels()
        {
            var allModels = (from m in dealerDb.CarModel
                             select m).ToList();

            return allModels;
        }

        public List<ContactUs> GetAllContacts()
        {
            var contacts = (from c in dealerDb.Contact
                            select c).ToList();

            return contacts;
        }

        public List<PurchaseType> GetAllPurchaseTypes()
        {
            var allTypes = (from p in dealerDb.Purchase
                            select p).ToList();

            return allTypes;
        }

        public IEnumerable<IdentityRole> GetAllRoles()
        {
            var roles = (from r in dealerDb.Roles
                         select r).ToList();

            return roles;
        }

        public List<AppUser> GetAllUsers()
        {
            var users = dealerDb.Users.ToList();
            var roles = dealerDb.Roles.ToList();

            foreach (var u in users)
            {
                foreach (var r in u.Roles)
                {
                    if (roles.Any(ur => ur.Id == r.RoleId))
                    {
                        var roleFound = roles.First(ur => ur.Id == r.RoleId);

                        u.Id = roleFound.Name;
                    }
                }
            }
            return users;
        }

        public List<VehicleDetails> GetAllVehicles(VehicleSearchInfo info)
        {
            var search = dealerDb.VehicleDetails.ToList().Where(s => s.IsSold == false && s.SalePrice >= info.MinPrice && s.SalePrice <= info.MaxPrice && s.Year >= info.MinYear && s.Year <= info.MaxYear).ToList();

            if (info.SearchBoxInfo == "empty")
            {
                search.ToList().OrderByDescending(m => m.MSRP);
                return search.Take(20).ToList();
            }

            else
            {
                return search.Where(s => s.ModelName.MakeName.Make.Contains(info.SearchBoxInfo) || s.ModelName.Model.Contains(info.SearchBoxInfo) || s.Year.ToString().Contains(info.SearchBoxInfo)).ToList();
            }
        }

        public List<VehicleDetails> GetAvailableVehicles()
        {
            var notSold = (from a in dealerDb.VehicleDetails
                           where a.IsSold == false
                           select a).ToList();

            return notSold;
        }

        public BodyStyle GetBodyStyleById(int id)
        {
            var bodyStyle = (from b in dealerDb.BodyStyle
                             where b.BodyStyleId == id
                             select b).FirstOrDefault();

            return bodyStyle;
        }

        public List<BodyStyle> GetBodyStyles()
        {
            var allBodies = (from b in dealerDb.BodyStyle
                             select b).ToList();

            return allBodies;
        }

        public ContactUs GetContactById(int id)
        {
            var contact = (from c in dealerDb.Contact
                           where c.CustomerContactId == id
                           select c).FirstOrDefault();

            return contact;
        }

        public List<VehicleDetails> GetFeaturedVehicles()
        {
            var featuredCars = (from f in dealerDb.VehicleDetails
                                where f.IsFeatured == true
                                select f).ToList();

            return featuredCars;
        }

        public CarMake GetMakeById(int id)
        {
            var makeById = (from m in dealerDb.CarMake
                            where m.MakeID == id
                            select m).FirstOrDefault();

            return makeById;
        }

        public CarModel GetModelById(int id)
        {
            var modelById = (from m in dealerDb.CarModel
                             where m.ModelID == id
                             select m).FirstOrDefault();

            return modelById;
        }

        public List<VehicleDetails> GetNewVehicles(VehicleSearchInfo info)
        {
            var search = GetAllVehicles(info).Where(s => s.IsNew && s.SalePrice >= info.MinPrice && s.SalePrice <= info.MaxPrice && s.Year >= info.MinYear && s.Year <= info.MaxYear).ToList();

            if (info.SearchBoxInfo == "empty")
            {
                search.ToList().OrderByDescending(m => m.MSRP);
                return search.Take(20).ToList();
            }

            else
            {
                return search.Where(s => s.ModelName.MakeName.Make.Contains(info.SearchBoxInfo) || s.ModelName.Model.Contains(info.SearchBoxInfo) || s.Year.ToString().Contains(info.SearchBoxInfo)).ToList();
            }
        }

        public PurchaseType GetPurchaseTypeById(int id)
        {
            var purchaseType = (from p in dealerDb.Purchase
                                where p.PurchaseTypeId == id
                                select p).FirstOrDefault();

            return purchaseType;
        }

        public List<Specials> GetSpecials()
        {
            var specials = (from s in dealerDb.Specials
                            select s).ToList();

            return specials;
        }

        public Specials GetSpecialsById(int id)
        {
            var specialsById = (from s in dealerDb.Specials
                                where s.SpecialsId == id
                                select s).FirstOrDefault();

            return specialsById;
        }

        public List<Transmission> GetTransmissions()
        {
            var tran = (from t in dealerDb.Transmission
                        select t).ToList();

            return tran;
        }

        public List<VehicleDetails> GetUsedVehicles(VehicleSearchInfo info)
        {
            var search = GetAllVehicles(info).Where(s => s.IsNew == false && s.SalePrice >= info.MinPrice && s.SalePrice <= info.MaxPrice && s.Year >= info.MinYear && s.Year <= info.MaxYear).ToList();

            if (info.SearchBoxInfo == "empty")
            {
                search.ToList().OrderByDescending(m => m.MSRP);
                return search.Take(20).ToList();
            }

            else
            {
                return search.Where(s => s.ModelName.MakeName.Make.Contains(info.SearchBoxInfo) || s.ModelName.Model.Contains(info.SearchBoxInfo) || s.Year.ToString().Contains(info.SearchBoxInfo)).ToList();
            }
        }

        public AppUser GetUserById(string id)
        {
            return dealerDb.Users.FirstOrDefault(u => u.Id == id);
            //var userById = (from u in dealerDb.Users
            //                where u.Id == id
            //                select u).FirstOrDefault();

            //return userById;
        }

        public VehicleDetails GetVehicleById(int id)
        {
            var carById = (from c in dealerDb.VehicleDetails
                           where c.VehicleId == id
                           select c).FirstOrDefault();

            return carById;
        }

        public List<VehicleType> GetVehicleType()
        {
            var carType = (from t in dealerDb.VehicleType
                           select t).ToList();

            return carType;
        }

        public void UpdatePassword(UserRoleViewModel viewModel)
        {
            var store = new UserStore<AppUser>(dealerDb);
            var userMgr = new UserManager<AppUser>(new UserStore<AppUser>(dealerDb));
            var roleMgr = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(dealerDb));

            viewModel.AppUser.PasswordHash = userMgr.PasswordHasher.HashPassword(viewModel.ConfirmPassword);

            userMgr.UpdateAsync(viewModel.AppUser);
            dealerDb.SaveChanges();
        }
    }
}
