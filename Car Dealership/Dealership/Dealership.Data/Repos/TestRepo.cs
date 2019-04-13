using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Dealership.Models;
using Dealership.Data.Interface;
using Dealership.Models.ViewModels;

namespace Dealership.Data.Repos
{
    public class TestRepo : IDealerRepo
    {
        //static List<SiteUser> _siteUser = new List<SiteUser>
        //{
        //    new SiteUser {FirstName="Javi", LastName="Aguirre", UserId=0},
        //    new SiteUser {FirstName="Reanna", LastName="Frigge", UserId=1},
        //    new SiteUser {FirstName="Chompiras", LastName="Pelado", UserId=2},
        //    new SiteUser {FirstName="", LastName="", UserId=3}
        //};

        static List<AppRole> _roles = new List<AppRole>
        {
            new AppRole {Id = "0", Name = "Admin" },
            new AppRole {Id = "1", Name = "Sales" },
            new AppRole {Id = "2", Name = "Disabled" }
        };

        static List<AppUser> _user = new List<AppUser>
        {
            new AppUser {FirstName="Javi", LastName="Aguirre", Email="Javi@guildcars.com",UserName="Javi@guildcars.com", Id="12345"},
            new AppUser {FirstName="Reanna", LastName="Frigge", Email="Reanna@guildcars.com",UserName="Reanna@guildcars.com",Id="123789"},
            new AppUser {FirstName="Chompiras", LastName="Pelado", Email="chumbo@guildcars.com",UserName="chumbo@guildcars.com",Id="61913"},
        };

        static List<ContactUs> _contacts = new List<ContactUs>
        {
            new ContactUs
            {
                Name ="El Stanko", Email="derp@yahoo.com", Phone="619-867-5309", Messsage="I need halpz with a vehicle!!", CustomerContactId=0
            }
        };

        //static List<CustomerInformation> _customerInfo = new List<CustomerInformation>
        //{
        //    new CustomerInformation
        //    {
        //        CustomerId=0, Name="El Stanko", Address="123 Random Rd", City="Sun Diego", State="CA", Zipcode=92154, Phone=619-867-5309, Email="derp@yahoo.com"
        //    }
        //};

        static List<CarMake> _carMake = new List<CarMake>
        {
            new CarMake {Make="Toyota", MakeID=0, DateAdded=DateTime.Parse("11/27/2017") }, //UserId=_siteUser[0]},
            new CarMake {Make="Renault", MakeID=1, DateAdded=DateTime.Parse("11/27/2017")}, //UserId=_siteUser[1]},
            new CarMake {Make="Koenigsegg", MakeID=2, DateAdded=DateTime.Parse("12/1/2017")}, //UserId=_siteUser[2]},
            new CarMake {Make="Reliant", MakeID=3, DateAdded=DateTime.Parse("12/3/2017")}, //UserId=_siteUser[3]}
            new CarMake {Make="Chevrolet", MakeID=4, DateAdded=DateTime.Parse("12/5/2017")},
            new CarMake {Make="Ford", MakeID=5, DateAdded=DateTime.Parse("12/5/2017")},
            new CarMake {Make="Ferrari", MakeID=6, DateAdded=DateTime.Parse("12/5/2017")},
            new CarMake {Make="Acura", MakeID=7, DateAdded=DateTime.Parse("12/5/2017")},
            new CarMake {Make="Mazda", MakeID=8, DateAdded=DateTime.Parse("12/5/2017")},
            new CarMake {Make="Dodge", MakeID=9, DateAdded=DateTime.Parse("12/5/2017")},
            new CarMake {Make="Nissan", MakeID=10, DateAdded=DateTime.Parse("12/5/2017")},
            new CarMake {Make="Honda", MakeID=11, DateAdded=DateTime.Parse("12/5/2017")},
            new CarMake {Make="Aston Martin", MakeID=12, DateAdded=DateTime.Parse("12/5/2017")}
        };

        static List<CarModel> _carModel = new List<CarModel>
        {
            new CarModel {Model="Celica", MakeName=_carMake[0], ModelID=0, DateAdded=DateTime.Parse("11/27/2017")},
            new CarModel {Model="Clio", MakeName=_carMake[1], ModelID=1, DateAdded=DateTime.Parse("11/27/2017")},
            new CarModel {Model="Regera", MakeName=_carMake[2], ModelID=2,DateAdded=DateTime.Parse("12/1/2017")},
            new CarModel {Model="Robin", MakeName=_carMake[3], ModelID=3,DateAdded=DateTime.Parse("12/3/2017")},
            new CarModel {Model="Avalanche", MakeName=_carMake[4], ModelID=4, DateAdded=DateTime.Parse("12/5/2017")},
            new CarModel {Model="Corolla", MakeName=_carMake[0], ModelID=5, DateAdded=DateTime.Parse("12/5/2017")},
            new CarModel {Model="F-150", MakeName=_carMake[5], ModelID=6, DateAdded=DateTime.Parse("12/5/2017")},
            new CarModel {Model="F430", MakeName=_carMake[6], ModelID=7, DateAdded=DateTime.Parse("12/5/2017")},
            new CarModel {Model="Integra", MakeName=_carMake[7], ModelID=8, DateAdded=DateTime.Parse("12/5/2017")},
            new CarModel {Model="Miata MX-5", MakeName=_carMake[8], ModelID=9, DateAdded=DateTime.Parse("12/5/2017")},
            new CarModel {Model="Neon", MakeName=_carMake[9], ModelID=10, DateAdded=DateTime.Parse("12/5/2017")},
            new CarModel {Model="Ram", MakeName=_carMake[9], ModelID=11, DateAdded=DateTime.Parse("12/5/2017")},
            new CarModel {Model="Silvia S15", MakeName=_carMake[10], ModelID=12, DateAdded=DateTime.Parse("12/5/2017")},
            new CarModel {Model="S2000", MakeName=_carMake[11], ModelID=13, DateAdded=DateTime.Parse("12/5/2017")},
            new CarModel {Model="V12 Vanquish", MakeName=_carMake[12], ModelID=14, DateAdded=DateTime.Parse("12/5/2017")},
            new CarModel {Model="Xterra", MakeName=_carMake[10], ModelID=15, DateAdded=DateTime.Parse("12/5/2017")}

        };

        static List<BodyStyle> _bodyStyle = new List<BodyStyle>
        {
            new BodyStyle {BodyType="Car", BodyStyleId=0},
            new BodyStyle {BodyType="Van", BodyStyleId=1},
            new BodyStyle {BodyType="SUV", BodyStyleId=2},
            new BodyStyle {BodyType="Truck", BodyStyleId=3},
        };

        static List<Transmission> _transmission = new List<Transmission>
        {
            new Transmission {TransmissionName="Automatic", TransmissionId=0},
            new Transmission {TransmissionName="Manual", TransmissionId=1}
        };

        static List<VehicleType> _vehicleType = new List<VehicleType>
        {
            new VehicleType {TypeDescription="New", VehicleTypeId=0},
            new VehicleType {TypeDescription="Used", VehicleTypeId=1}
        };


        static List<VehicleDetails> _vehicleDetails = new List<VehicleDetails>
        {
            //mine
            new VehicleDetails {VehicleId=0, VIN="311xxRHCP2319TGIF", ModelName=_carModel[0], Year=2001, Color="White",
                BodyName =_bodyStyle[0], TransmissionType=_transmission[1], Interior="Black", VehicleType=_vehicleType[0], Mileage=619, MSRP=25624, SalePrice=21000,
                Description="Dream cars are the ones that take you back to your youth and bring back that smile. Prepare to smile with the " +
                "sporty TRD edition of the Celica with 5 speed manual transmission, race inspired bucket seats on a V4, 2.2L, gas efficient engine.",
                IsFeatured=true, IsNew=true, ImageUrl="http://localhost:50822/Images/celica.jpg", IsSold=false},

            //Renault
            new VehicleDetails {VehicleId=1, VIN="830POO1130YAY1205", ModelName=_carModel[1], Year=2005, Color="Saphire Blue",
                BodyName =_bodyStyle[0], TransmissionType=_transmission[1], Interior="Black", VehicleType=_vehicleType[1], Mileage=1952, MSRP=17000, SalePrice=13095,
                Description="In traditional French fashion, prepare to surrender...your love for cars. The sleek, quick and agile Clio will zip " +
                "you around town like no other with its V6, 6 speed gear-box that gives you a nice 24mpg on average. Don't forget your electronics "
                + "as it is entirely blu-tooth compatible.", IsFeatured=false, IsNew=false, ImageUrl="http://localhost:50822/Images/clio.jpg", IsSold=false},

            //tio y tia
            new VehicleDetails {VehicleId=2, VIN="502TIO91911TIA123", ModelName=_carModel[4], Year=2007, Color="Orange",
                BodyName=_bodyStyle[3], TransmissionType=_transmission[0], Interior="Black leather", VehicleType=_vehicleType[1], Mileage=120000,
                MSRP=23518, SalePrice=19875, Description="Un homenaje a mi tia y tio por todo lo que me dieron durante todos estos anos y por seguir " +
                "apoyandome a cumplir mis suenos de terminar mis estudios y consejos para la vida. Los quiero mucho a los dos.", IsFeatured=true,
                IsNew=false, ImageUrl ="http://localhost:50822/Images/Avalanche.jpg", IsSold=true},

            //Lola
            new VehicleDetails {VehicleId=3, VIN="0624LOLA0409PIPER", ModelName=_carModel[5], Year=2009, Color="Silver",
                BodyName=_bodyStyle[0], TransmissionType=_transmission[0], Interior="Gray", VehicleType=_vehicleType[1], Mileage=126753, MSRP=15528,
                SalePrice=12345, Description="Lola The Corolla... has quite the ring to it. Reliable, gas efficient and a great overall daily driver, " +
                "this little car and its 1.8L, V4 engine will get you around town on average with 27MPG. Comes standard with CD player and a " +
                "AUX inut for that friend who claims their Soundcloud is fire (sigh).", IsFeatured=true, IsNew=false,
                 IsSold=true, ImageUrl ="http://localhost:50822/Images/Corolla.jpg"},

            //Stanky
            new VehicleDetails {VehicleId=4, VIN="0901DERPXB360LEON", ModelName=_carModel[6], Year=2002, Color="White",
                BodyName=_bodyStyle[3], TransmissionType=_transmission[0], Interior="Suede", VehicleType=_vehicleType[0], Mileage=711,
                MSRP=12000, SalePrice=10000, Description="Everyone needs a friend with a truck because not everyone thinks that they will EVER need to " +
                "move or purchase a new bed or a giant tv they don't need, that is where you come in. This F-150 comes with an extended cab that is sure " +
                "to help all your friends (and your momma too) when the time comes. 2.5 inch lift kit installed with auto-drop bars to climb into your " +
                "mini-monster truck as you ravage through the streets burning your amazing 16MPG with a fat Flowmaster exhaust kit installed.",
                IsFeatured=false, IsNew=true, IsSold=false, ImageUrl ="http://localhost:50822/Images/f150.jpg"},

            //Ponch
            new VehicleDetails {VehicleId=5, VIN="1218PONCH733GEARS", ModelName=_carModel[7], Year=2000, Color="Red",
                BodyName=_bodyStyle[0], TransmissionType=_transmission[1], Interior="Black, Tan leather", VehicleType=_vehicleType[0], Mileage=430,
                MSRP=87000, SalePrice=83500, Description="Get ready to feel the pow-a!!! The 4.3L, V8 F430 roars through the streets like a lion " +
                "stalking its prey. Duo-quad pipe exhaust with a riveting 480hp, mid-rear engine mounted for perfect weight distribution to handle the " +
                "sharpest of turns and quick launch to get you 0-60 in just 3.6 seconds.", IsFeatured=true, IsNew=true,
                IsSold=false, ImageUrl ="http://localhost:50822/Images/f430.jpg"},

            //Integra
            new VehicleDetails {VehicleId=6, VIN="123RANDOM456VIN#", ModelName=_carModel[8], Year=2001, Color="Black",
                BodyName=_bodyStyle[0], TransmissionType=_transmission[1], Interior="Black", VehicleType=_vehicleType[1], Mileage=3300,
                MSRP=7500, SalePrice=6700, Description="V-TEC KICKED IN YO!!!! The Integra Type-R at first seems like a small car with not a ton to offer " + 
                "beyond its historic name but what you don't know is underhood runs a 1.8L, V4 DOHC V-tec engine powered by Mugen (no pun intended). " + 
                "The Japanese import is to much fun to drive and just remember to reve high!", IsFeatured=false, IsNew=false,
                 IsSold=false, ImageUrl ="http://localhost:50822/Images/Integra.jpg"},

            //HAPPY!!!!!
            new VehicleDetails {VehicleId=7, VIN="987RANDO654NUMBER", ModelName=_carModel[9], Year=2015, Color="Red",
                BodyName=_bodyStyle[0], TransmissionType=_transmission[1], Interior="Black", VehicleType=_vehicleType[1], Mileage=2300,
                MSRP=19000, SalePrice=17500, Description="Smile like you mean it! It is always better to drive a slow car fast than it is to " +
                "drive a fast car slow. The Miata MX-5 will be that car you can drive fast and make you smile just the way it smile when you look at it " + 
                "from the front. The original Japanese roadster comes with a new 2.0L, I4 engine in its new model with a quick 6 speed manual gearbox and " +
                "a removable hard top for enjoying the sun while coasting in style.", IsFeatured=false, IsNew=false,
                IsSold=false, ImageUrl ="http://localhost:50822/Images/mx5.jpg"},

            //Tomcat 
            new VehicleDetails {VehicleId=8, VIN="TERRYFREI14886969", ModelName=_carModel[10], Year=2005, Color="Yellow",
                BodyName=_bodyStyle[0], TransmissionType=_transmission[1], Interior="Black", VehicleType=_vehicleType[0], Mileage=722,
                MSRP=24500, SalePrice=20000, Description="The SRT-4 was meant for speed right out of the box, taking a 2.4L, V4 engine SOHC with a " + 
                "turbocharged engine that runs up to 14psi boost and a large intake to keep it cool. The power output let it go 0-60 in 5.3 using " +
                "the downforce from the giant SPOILER ALERT!!! in the back of the car. Grip it and rip it!!", IsFeatured=false,
                IsNew=true, IsSold=false, ImageUrl ="http://localhost:50822/Images/Neon.jpg"},

            //Ram
            new VehicleDetails {VehicleId=9, VIN="HUGE1500TRUCK11210", ModelName=_carModel[11], Year=2009, Color="Silver",
                BodyName=_bodyStyle[3], TransmissionType=_transmission[0], Interior="Brown", VehicleType=_vehicleType[1], Mileage=19528,
                MSRP=26000, SalePrice=24000, Description="RAM through your workload in this high powered, extended cab 1500. Honing a powerful 3.7L " +
                "V6 sized engine, you can get up to 6 tons of towing capacity in case you decide someone took your parking spot and want to show them " +
                "the rules of the road or an of your hauling needs.", IsFeatured=false, IsNew=false, IsSold=false,
                ImageUrl ="http://localhost:50822/Images/Ram.jpg"},

            new VehicleDetails {VehicleId=10, ImageUrl="http://localhost:50822/Images/Regera.jpg"},

            new VehicleDetails {VehicleId=11, ImageUrl="http://localhost:50822/Images/Robin.jpg"},

            new VehicleDetails {VehicleId=12, ImageUrl="http://localhost:50822/Images/s15.jpg"},

            new VehicleDetails {VehicleId=13, ImageUrl="http://localhost:50822/Images/s2k.jpg"},

            new VehicleDetails {VehicleId=14, ImageUrl="http://localhost:50822/Images/Vanquish.jpg"},

            new VehicleDetails {VehicleId=15, ImageUrl="http://localhost:50822/Images/xterra.jpg"},

            

        };

        static List<PurchaseType> _purchaseType = new List<PurchaseType>
        {
            new PurchaseType {Type="Cash", PurchaseTypeId=0},
            new PurchaseType {Type="Bank Loan", PurchaseTypeId=1},
            new PurchaseType {Type="Dealer Finance", PurchaseTypeId=2}
        };

        static List<Specials> _specials = new List<Specials>
        {
            new Specials {SpecialName="Weekend Warrior", Details="Come into our dealership and get 15% off your total cost of a vehicle purchase.", SpecialsId=0},
            new Specials {SpecialName="Big Ballerz Club", Details="Purchase 2 or more cars in your visit and we will do ALL repairs of ANY kind until you pay them off.", SpecialsId=1},
        };

        static List<Sale> _carSale = new List<Sale>
        {
            new Sale
            {
                SaleId=1,
                VehicleId=0,
                DateSold=DateTime.Parse("11/25/2017"),
                PriceSold= 21000,
                PurchaseType=_purchaseType[1],
                User=_user[2],
                Name ="El Stanko",
                Address ="123 Random Rd",
                City ="Sun Diego",
                State ="CA",
                Zipcode =92154,
                Phone =619-867-5309,
                Email ="derp@yahoo.com"
            }
        };

        public void AddPurchase(PurchaseViewModel viewModel)
        {
            Sale newPurchase = viewModel.SaleDetails;
            newPurchase.DateSold = DateTime.Now;
            newPurchase.User = viewModel.SaleDetails.User;
            newPurchase.SaleId = _carSale.Max(p => p.SaleId) + 1;
            newPurchase.PurchaseType = _purchaseType.SingleOrDefault(p => p.PurchaseTypeId == viewModel.PurchaseTypeDetails.PurchaseTypeId);
            VehicleDetails sold = _vehicleDetails.SingleOrDefault(v => v.VehicleId == viewModel.VehiclePurchaseId);
            newPurchase.IsSold = true;
            newPurchase.IsFeatured = false;
            _vehicleDetails.RemoveAll(v => v.VehicleId == viewModel.VehiclePurchaseId);
            _vehicleDetails.Add(sold);
            _carSale.Add(newPurchase);
        }

        public void CreateMake(CarMake newMake)
        {
            if (_carMake.Count <= 0)
            {
                newMake.MakeID = 1;
            }

            else
            {
                newMake.MakeID = _carMake.Max(c => c.MakeID) + 1;
            }

            _carMake.Add(newMake);
        }

        public void CreateModel(CarModel newModel)
        {
            if (_carModel.Count <= 0)
            {
                newModel.ModelID = 1;
            }

            else
            {
                newModel.ModelID = _carModel.Max(c => c.ModelID) + 1;
            }

            _carModel.Add(newModel);
        }

        public void CreateSpecial(Specials newSpecial)
        {
            throw new NotImplementedException();
        }

        public void CreateUser(UserRoleViewModel viewModel)
        {
            var newUser = viewModel.AppUser;
            _user.Add(newUser);
        }

        public void CreateVehicle(AddVehicleViewModel newVehicle)
        {
            if (_vehicleDetails.Count <= 0)
            {
                newVehicle.Vehicle.VehicleId = 1;
            }

            else
            {
                newVehicle.Vehicle.VehicleId = _vehicleDetails.Max(c => c.VehicleId) + 1;
            }

            newVehicle.Vehicle.ModelName = _carModel.FirstOrDefault(m => m.ModelID == newVehicle.Vehicle.ModelName.ModelID);
            newVehicle.Vehicle.TransmissionType = _transmission.FirstOrDefault(m => m.TransmissionId == newVehicle.Vehicle.TransmissionType.TransmissionId);
            newVehicle.Vehicle.BodyName = _bodyStyle.FirstOrDefault(m => m.BodyStyleId == newVehicle.Vehicle.BodyName.BodyStyleId);

            _vehicleDetails.Add(newVehicle.Vehicle);
        }

        public bool DeleteMake(int deleteMake)
        {
            _carMake.RemoveAll(m => m.MakeID == deleteMake);
            return true;
        }

        public bool DeleteModel(int deleteModel)
        {
            _carModel.RemoveAll(m => m.ModelID == deleteModel);
            return true;
        }

        public void DeleteSpecial(Specials deleteSpecial)
        {
            throw new NotImplementedException();
        }

        public void Deletevehicle(int deleteVehicle)
        {
            _vehicleDetails.RemoveAll(v => v.VehicleId == deleteVehicle);
        }

        public void EditMake(CarMake editMake)
        {
            _carMake.RemoveAll(m => m.MakeID == editMake.MakeID);
            _carMake.Add(editMake);
        }

        public void EditModel(CarModel editModel)
        {
            _carModel.RemoveAll(m => m.ModelID == editModel.ModelID);
            _carModel.Add(editModel);
        }

        public void EditSpecial(Specials editSpecial)
        {
            throw new NotImplementedException();
        }

        public void EditUser(UserRoleViewModel viewModel)
        {
            var edit = viewModel.AppUser;
            _user.RemoveAll(u => u.Id == viewModel.AppUser.Id);
            _user.Add(edit);
        }

        public void EditVehicle(EditVehicleViewModel editVehicle)
        {
            editVehicle.Vehicle.ModelName = _carModel.FirstOrDefault(m => m.ModelID == editVehicle.Vehicle.ModelName.ModelID);
            editVehicle.Vehicle.TransmissionType = _transmission.FirstOrDefault(m => m.TransmissionId == editVehicle.Vehicle.TransmissionType.TransmissionId);
            editVehicle.Vehicle.BodyName = _bodyStyle.FirstOrDefault(m => m.BodyStyleId == editVehicle.Vehicle.BodyName.BodyStyleId);

            _vehicleDetails.RemoveAll(v => v.VehicleId == editVehicle.Vehicle.VehicleId);
            _vehicleDetails.Add(editVehicle.Vehicle);
        }

        public List<CarMake> GetAllCarMakes()
        {
            return _carMake;
        }

        public List<CarModel> GetAllCarModels()
        {
            return _carModel;
        }

        public List<ContactUs> GetAllContacts()
        {
            return _contacts;
        }

        public List<PurchaseType> GetAllPurchaseTypes()
        {
            return _purchaseType;
        }

        public IEnumerable<IdentityRole> GetAllRoles()
        {
            return _roles.ToList();
        }

        public List<AppUser> GetAllUsers()
        {
            return _user;
        }

        public List<VehicleDetails> GetAllVehicles(VehicleSearchInfo info)
        {
            return _vehicleDetails;
        }

        public List<VehicleDetails> GetAvailableVehicles()
        {
            return _vehicleDetails.Where(s => s.IsSold==false).ToList();
        }

        public BodyStyle GetBodyStyleById(int id)
        {
            return _bodyStyle.FirstOrDefault(b => b.BodyStyleId == id);
        }

        public List<BodyStyle> GetBodyStyles()
        {
            return _bodyStyle;
        }

        public ContactUs GetContactById(int id)
        {
            return _contacts.FirstOrDefault(c => c.CustomerContactId == id);
        }

        public List<VehicleDetails> GetFeaturedVehicles()
        {
            return _vehicleDetails.Where(f => f.IsFeatured).ToList();
        }

        public CarMake GetMakeById(int id)
        {
            return _carMake.FirstOrDefault(m => m.MakeID == id);
        }

        public CarModel GetModelById(int id)
        {
            return _carModel.FirstOrDefault(m => m.ModelID == id);
        }

        public List<VehicleDetails> GetNewVehicles(VehicleSearchInfo info)
        {
            var search = _vehicleDetails.Where(s => s.IsNew && s.SalePrice >= info.MinPrice && s.SalePrice <= info.MaxPrice && s.Year >= info.MinYear && s.Year <= info.MaxYear).ToList();

            if (info.SearchBoxInfo == "empty")
            {
                search.OrderByDescending(m => m.MSRP).ToList();
                return search;
            }

            else
            {
                return search.Where(s => s.ModelName.MakeName.Make.Contains(info.SearchBoxInfo) || s.ModelName.Model.Contains(info.SearchBoxInfo) || s.Year.ToString().Contains(info.SearchBoxInfo)).ToList();
            }
        }

        public PurchaseType GetPurchaseTypeById(int id)
        {
            return _purchaseType.FirstOrDefault(p => p.PurchaseTypeId == id);
        }

        public List<Specials> GetSpecials()
        {
            return _specials;
        }

        public Specials GetSpecialsById(int id)
        {
            return _specials.FirstOrDefault(s => s.SpecialsId == id);
        }

        public List<Transmission> GetTransmissions()
        {
            return _transmission;
        }

        public List<VehicleDetails> GetUsedVehicles(VehicleSearchInfo info)
        {
            var search = _vehicleDetails.Where(s => s.IsNew == false && s.SalePrice >= info.MinPrice && s.SalePrice <= info.MaxPrice && s.Year >= info.MinYear && s.Year <= info.MaxYear).ToList();

            if (info.SearchBoxInfo == "empty")
            {
                search.OrderByDescending(m => m.MSRP).ToList();
                return search;
            }

            else
            {
                return search.Where(s => s.ModelName.MakeName.Make.Contains(info.SearchBoxInfo) || s.ModelName.Model.Contains(info.SearchBoxInfo) || s.Year.ToString().Contains(info.SearchBoxInfo)).ToList();
            }
        }

        public AppUser GetUserById(string id)
        {
            return _user.FirstOrDefault(u => u.Id == id);
        }

        public VehicleDetails GetVehicleById(int id)
        {
            return _vehicleDetails.FirstOrDefault(v => v.VehicleId == id);
        }

        public List<VehicleType> GetVehicleType()
        {
            return _vehicleType;
        }

        public void UpdatePassword(UserRoleViewModel viewModel)
        {
            throw new NotImplementedException();
        }
    }
}
