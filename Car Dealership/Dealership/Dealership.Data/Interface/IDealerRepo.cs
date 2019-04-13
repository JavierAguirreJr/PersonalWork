using Dealership.Models;
using Dealership.Models.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dealership.Data.Interface
{
    public interface IDealerRepo
    {
        List<AppUser> GetAllUsers();
        AppUser GetUserById(string id);
        void CreateUser(UserRoleViewModel viewModel);
        void EditUser(UserRoleViewModel viewModel);
        void UpdatePassword(UserRoleViewModel viewModel);
        IEnumerable<IdentityRole> GetAllRoles();

        List<VehicleDetails> GetAllVehicles(VehicleSearchInfo info);
        List<VehicleDetails> GetAvailableVehicles();
        List<VehicleDetails> GetFeaturedVehicles();
        List<VehicleDetails> GetNewVehicles(VehicleSearchInfo info);
        List<VehicleDetails> GetUsedVehicles(VehicleSearchInfo info);
        List<Transmission> GetTransmissions();
        List<VehicleType> GetVehicleType();

        VehicleDetails GetVehicleById(int id);
        void CreateVehicle(AddVehicleViewModel newVehicle);
        void EditVehicle(EditVehicleViewModel editVehicle);
        void Deletevehicle(int deleteVehicle);

        List<CarMake> GetAllCarMakes();
        CarMake GetMakeById(int id);
        void CreateMake(CarMake newMake);
        void EditMake(CarMake editMake);
        bool DeleteMake(int deleteMake);

        List<CarModel> GetAllCarModels();
        CarModel GetModelById(int id);
        void CreateModel(CarModel newModel);
        void EditModel(CarModel editModel);
        bool DeleteModel(int deleteModel);

        List<BodyStyle> GetBodyStyles();
        BodyStyle GetBodyStyleById(int id);

        List<Specials> GetSpecials();
        Specials GetSpecialsById(int id);
        void CreateSpecial(Specials newSpecial);
        void EditSpecial(Specials editSpecial);
        void DeleteSpecial(Specials deleteSpecial);

        List<ContactUs> GetAllContacts();
        ContactUs GetContactById(int id);

        List<PurchaseType> GetAllPurchaseTypes();
        PurchaseType GetPurchaseTypeById(int id);
        void AddPurchase(PurchaseViewModel purchase);
    }
}
