namespace Dealership.Data.Migrations
{
    using Dealership.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Dealership.Models.CarDealershipDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Dealership.Models.CarDealershipDbContext context)
        {
            //if (!System.Diagnostics.Debugger.IsAttached)
            //{
            //    System.Diagnostics.Debugger.Launch();
            //}

            //Load the user and role managers with our custom models
            var userMgr = new UserManager<AppUser>(new UserStore<AppUser>(context));
            var roleMgr = new RoleManager<AppRole>(new RoleStore<AppRole>(context));

            //create the admin role
            if (!roleMgr.RoleExists("admin"))
            {
                roleMgr.Create(new AppRole() { Name = "admin" });
            }
            if(userMgr.FindByName("Javi@guildcars.com")==null)
            {
                var newAdmin = new AppUser()
                {
                    FirstName = "Javi",
                    LastName = "Aguirre",
                    UserName = "Javi@guildcars.com",
                    Email = "Javi@guildcars.com",
                    //RoleName="admin"
                };
                userMgr.Create(newAdmin, "testing123");
            }

            var addAdmin = userMgr.FindByName("Javi@guildcars.com");

            if(!userMgr.IsInRole(addAdmin.Id, "admin"))
            {
                userMgr.AddToRole(addAdmin.Id, "admin");
            }
                
            //-----------------------------------------------------------------------------------------------------------

            //create the sales
            if (!roleMgr.RoleExists("sales"))
            {
                roleMgr.Create(new AppRole() { Name = "sales" });
            }
            if(userMgr.FindByName("Reanna@guildcars.com")==null)
            {
                var newSales = new AppUser()
                {
                    FirstName = "Reana",
                    LastName = "Frigge",
                    UserName = "Reanna@guildcars.com",
                    Email = "Reanna@guildcars.com",
                    //RoleName="sales"
                    
                };
                userMgr.Create(newSales, "testing123");
            }

            var addSales = userMgr.FindByEmail("Reanna@guildcars.com");

            if(!userMgr.IsInRole(addSales.Id, "sales"))
            {
                userMgr.AddToRole(addSales.Id, "sales");
            }

            //-----------------------------------------------------------------------------------------------------------

            //create disabled user
            if (!roleMgr.RoleExists("Disabled"))
            {
                roleMgr.Create(new AppRole() { Name = "Disabled" });
            }
            if (userMgr.FindByName("chumbo@guildcars.com") == null)
            {
                var newUser = new AppUser()
                {
                    FirstName = "Chompiras",
                    LastName = "Pelado",
                    UserName = "chumbo@guildcars.com",
                    Email = "chumbo@guildcars.com",
                    //RoleName="Disabled",
                };
                userMgr.Create(newUser, "nope123");
            }

            var disabledUser = userMgr.FindByName("chumbo@guildcars.com");
            if (!userMgr.IsInRole(disabledUser.Id, "Disabled"))
            {
                userMgr.AddToRole(disabledUser.Id, "Disabled");
            }

            context.SaveChanges();

            //purchase types
            context.Purchase.AddOrUpdate(
                p => p.Type,
                new PurchaseType { Type = "Cash"},
                new PurchaseType { Type = "Bank Loan" },
                new PurchaseType { Type = "Dealer Finance" }
            );

            context.SaveChanges();

            //specials
            context.Specials.AddOrUpdate(
                s => s.SpecialName,
                 new Specials { SpecialName = "Weekend Warrior", Details = "Come into our dealership and get 15% off your total cost of a vehicle purchase."},
                 new Specials { SpecialName = "Big Ballerz Club", Details = "Purchase 2 or more cars in your visit and we will do ALL repairs of ANY kind until you pay them off."}
            );

            context.SaveChanges();

            //makes
            context.CarMake.AddOrUpdate(
                m => m.Make,
            new CarMake { Make = "Toyota", DateAdded = DateTime.Parse("11/27/2017"), User=context.Users.FirstOrDefault(u=>u.UserName=="Javi@guildcars.com")}, //UserId=_siteUser[0]},
            new CarMake { Make = "Renault", DateAdded = DateTime.Parse("11/27/2017"), User = context.Users.FirstOrDefault(u => u.UserName == "Javi@guildcars.com") }, //UserId=_siteUser[1]},
            new CarMake { Make = "Koenigsegg", DateAdded = DateTime.Parse("12/1/2017"), User = context.Users.FirstOrDefault(u => u.UserName == "Javi@guildcars.com") }, //UserId=_siteUser[2]},
            new CarMake { Make = "Reliant", DateAdded = DateTime.Parse("12/3/2017"), User = context.Users.FirstOrDefault(u => u.UserName == "Javi@guildcars.com") }, //UserId=_siteUser[3]}
            new CarMake { Make = "Chevrolet", DateAdded = DateTime.Parse("12/5/2017"), User = context.Users.FirstOrDefault(u => u.UserName == "Javi@guildcars.com") },
            new CarMake { Make = "Ford", DateAdded = DateTime.Parse("12/5/2017"), User = context.Users.FirstOrDefault(u => u.UserName == "Javi@guildcars.com") },
            new CarMake { Make = "Ferrari", DateAdded = DateTime.Parse("12/5/2017"), User = context.Users.FirstOrDefault(u => u.UserName == "Javi@guildcars.com") },
            new CarMake { Make = "Acura", DateAdded = DateTime.Parse("12/5/2017"), User = context.Users.FirstOrDefault(u => u.UserName == "Javi@guildcars.com") },
            new CarMake { Make = "Mazda", DateAdded = DateTime.Parse("12/5/2017"), User = context.Users.FirstOrDefault(u => u.UserName == "Javi@guildcars.com") },
            new CarMake { Make = "Dodge", DateAdded = DateTime.Parse("12/5/2017"), User = context.Users.FirstOrDefault(u => u.UserName == "Javi@guildcars.com") },
            new CarMake { Make = "Nissan", DateAdded = DateTime.Parse("12/5/2017"), User = context.Users.FirstOrDefault(u => u.UserName == "Javi@guildcars.com") },
            new CarMake { Make = "Honda", DateAdded = DateTime.Parse("12/5/2017"), User = context.Users.FirstOrDefault(u => u.UserName == "Javi@guildcars.com") },
            new CarMake { Make = "Aston Martin", DateAdded = DateTime.Parse("12/5/2017"), User = context.Users.FirstOrDefault(u => u.UserName == "Javi@guildcars.com") }
            );

            context.SaveChanges();

            //models
            context.CarModel.AddOrUpdate(
                m => m.Model,
            new CarModel { Model = "Celica", MakeName = context.CarMake.FirstOrDefault(n=>n.Make=="Toyota"), DateAdded = DateTime.Parse("11/27/2017"), User = context.Users.FirstOrDefault(u => u.UserName == "Javi@guildcars.com") },
            new CarModel { Model = "Clio", MakeName = context.CarMake.FirstOrDefault(n => n.Make=="Renault"),  DateAdded = DateTime.Parse("11/27/2017"), User = context.Users.FirstOrDefault(u => u.UserName == "Javi@guildcars.com") },
            new CarModel { Model = "Regera", MakeName = context.CarMake.FirstOrDefault(n => n.Make== "Koenigsegg"), DateAdded = DateTime.Parse("12 /1/2017"), User = context.Users.FirstOrDefault(u => u.UserName == "Javi@guildcars.com") },
            new CarModel { Model = "Robin", MakeName = context.CarMake.FirstOrDefault(n => n.Make=="Reliant"), DateAdded = DateTime.Parse("12/3/2017"), User = context.Users.FirstOrDefault(u => u.UserName == "Javi@guildcars.com") },
            new CarModel { Model = "Avalanche", MakeName = context.CarMake.FirstOrDefault(n => n.Make=="Chevrolet"), DateAdded = DateTime.Parse("12/5/2017"), User = context.Users.FirstOrDefault(u => u.UserName == "Javi@guildcars.com") },
            new CarModel { Model = "Corolla", MakeName = context.CarMake.FirstOrDefault(n => n.Make=="Toyota"), DateAdded = DateTime.Parse("12/5/2017"), User = context.Users.FirstOrDefault(u => u.UserName == "Javi@guildcars.com") },
            new CarModel { Model = "F-150", MakeName = context.CarMake.FirstOrDefault(n => n.Make=="Ford"), DateAdded = DateTime.Parse("12/5/2017"), User = context.Users.FirstOrDefault(u => u.UserName == "Javi@guildcars.com") },
            new CarModel { Model = "F430", MakeName = context.CarMake.FirstOrDefault(n => n.Make=="Ferrari"), DateAdded = DateTime.Parse("12/5/2017"), User = context.Users.FirstOrDefault(u => u.UserName == "Javi@guildcars.com") },
            new CarModel { Model = "Integra", MakeName = context.CarMake.FirstOrDefault(n => n.Make=="Acura"), DateAdded = DateTime.Parse("12/5/2017"), User = context.Users.FirstOrDefault(u => u.UserName == "Javi@guildcars.com") },
            new CarModel { Model = "Miata MX-5", MakeName = context.CarMake.FirstOrDefault(n => n.Make=="Mazda"), DateAdded = DateTime.Parse("12/5/2017"), User = context.Users.FirstOrDefault(u => u.UserName == "Javi@guildcars.com") },
            new CarModel { Model = "Neon", MakeName = context.CarMake.FirstOrDefault(n => n.Make=="Dodge"), DateAdded = DateTime.Parse("12/5/2017"), User = context.Users.FirstOrDefault(u => u.UserName == "Javi@guildcars.com") },
            new CarModel { Model = "Ram", MakeName = context.CarMake.FirstOrDefault(n => n.Make=="Dodge"), DateAdded = DateTime.Parse("12/5/2017"), User = context.Users.FirstOrDefault(u => u.UserName == "Javi@guildcars.com") },
            new CarModel { Model = "Silvia S15", MakeName = context.CarMake.FirstOrDefault(n => n.Make=="Nissan"), DateAdded = DateTime.Parse("12/5/2017"), User = context.Users.FirstOrDefault(u => u.UserName == "Javi@guildcars.com") },
            new CarModel { Model = "S2000", MakeName = context.CarMake.FirstOrDefault(n => n.Make=="Nissan"), DateAdded = DateTime.Parse("12/5/2017"), User = context.Users.FirstOrDefault(u => u.UserName == "Javi@guildcars.com") },
            new CarModel { Model = "V12 Vanquish", MakeName = context.CarMake.FirstOrDefault(n => n.Make=="Aston Martin"), DateAdded = DateTime.Parse("12/5/2017"), User = context.Users.FirstOrDefault(u => u.UserName == "Javi@guildcars.com") },
            new CarModel { Model = "Xterra", MakeName = context.CarMake.FirstOrDefault(n => n.Make=="Nissan"), DateAdded = DateTime.Parse("12/5/2017"), User = context.Users.FirstOrDefault(u => u.UserName == "Javi@guildcars.com") }
            );

            context.SaveChanges();

            //types
            context.VehicleType.AddOrUpdate(
                t => t.TypeDescription,
                new VehicleType { TypeDescription = "New" },
                new VehicleType { TypeDescription = "Used" }
            );

            context.SaveChanges();

            //transmissions
            context.Transmission.AddOrUpdate(
                t => t.TransmissionName,
                new Transmission { TransmissionName = "Automatic" },
                new Transmission { TransmissionName = "Manual" }
            );

            context.SaveChanges();

            //body styles
            context.BodyStyle.AddOrUpdate(
                b => b.BodyType,
                new BodyStyle { BodyType = "Truck" },
                new BodyStyle { BodyType = "Car" },
                new BodyStyle { BodyType = "SUV" },
                new BodyStyle { BodyType = "Van" }
            );

            context.SaveChanges();

            //THE vehicles
            context.VehicleDetails.AddOrUpdate(
                v => v.VehicleId,
            //mine
            new VehicleDetails
            {
                VehicleId = 0,
                VIN = "311xxRHCP2319TGIF",
                ModelName = context.CarModel.FirstOrDefault(n => n.Model == "Celica"),
                Year = 2001,
                Color = "White",
                BodyName = context.BodyStyle.FirstOrDefault(b => b.BodyType=="Car"),
                TransmissionType = context.Transmission.FirstOrDefault(t=>t.TransmissionName=="Manual"),
                Interior = "Black",
                VehicleType = context.VehicleType.FirstOrDefault(v=>v.TypeDescription=="New"),
                Mileage = 619,
                MSRP = 25624,
                SalePrice = 21000,
                Description = "Dream cars are the ones that take you back to your youth and bring back that smile. Prepare to smile with the " +
                "sporty TRD edition of the Celica with 5 speed manual transmission, race inspired bucket seats on a V4, 2.2L, gas efficient engine.",
                IsFeatured = true,
                IsNew = true,
                ImageUrl = "http://localhost:50822/Images/celica.jpg",
                IsSold = false
            },

            //Renault
            new VehicleDetails
            {
                VehicleId = 1,
                VIN = "830POO1130YAY1205",
                ModelName = context.CarModel.FirstOrDefault(n=>n.Model=="Clio"),
                Year = 2005,
                Color = "Saphire Blue",
                BodyName = context.BodyStyle.FirstOrDefault(b=>b.BodyType=="Car"),
                TransmissionType = context.Transmission.FirstOrDefault(t=>t.TransmissionName=="Manual"),
                Interior = "Black",
                VehicleType = context.VehicleType.FirstOrDefault(v=>v.TypeDescription=="Used"),
                Mileage = 1952,
                MSRP = 17000,
                SalePrice = 13095,
                Description = "In traditional French fashion, prepare to surrender...your love for cars. The sleek, quick and agile Clio will zip " +
                "you around town like no other with its V6, 6 speed gear-box that gives you a nice 24mpg on average. Don't forget your electronics "
                + "as it is entirely blu-tooth compatible.",
                IsFeatured = false,
                IsNew = false,
                ImageUrl = "http://localhost:50822/Images/clio.jpg",
                IsSold = false
            },

            //tio y tia
            new VehicleDetails
            {
                VehicleId = 2,
                VIN = "502TIO91911TIA123",
                ModelName = context.CarModel.FirstOrDefault(n => n.Model=="AValanche"),
                Year = 2007,
                Color = "Orange",
                BodyName = context.BodyStyle.FirstOrDefault(b=>b.BodyType=="Truck"),
                TransmissionType = context.Transmission.FirstOrDefault(t=>t.TransmissionName=="Automatic"),
                Interior = "Black Leather",
                VehicleType = context.VehicleType.FirstOrDefault(v=>v.TypeDescription=="Used"),
                Mileage = 120000,
                MSRP = 23518,
                SalePrice = 19875,
                Description = "Un homenaje a mi tia y tio por todo lo que me dieron durante todos estos anos y por seguir " +
                "apoyandome a cumplir mis suenos de terminar mis estudios y consejos para la vida. Los quiero mucho a los dos.",
                IsFeatured = true,
                IsNew = false,
                ImageUrl = "http://localhost:50822/Images/Avalanche.jpg",
                IsSold = true
            },

            //Lola
            new VehicleDetails
            {
                VehicleId = 3,
                VIN = "0624LOLA0409PIPER",
                ModelName = context.CarModel.FirstOrDefault(m=>m.Model=="Corolla"),
                Year = 2009,
                Color = "Gray",
                BodyName = context.BodyStyle.FirstOrDefault(b=>b.BodyType=="Car"),
                TransmissionType = context.Transmission.FirstOrDefault(t=>t.TransmissionName=="Automatic"),
                Interior = "Gray",
                VehicleType = context.VehicleType.FirstOrDefault(v=>v.TypeDescription=="Used"),
                Mileage = 126753,
                MSRP = 15528,
                SalePrice = 12345,
                Description = "Lola The Corolla... has quite the ring to it. Reliable, gas efficient and a great overall daily driver, " +
                "this little car and its 1.8L, V4 engine will get you around town on average with 27MPG. Comes standard with CD player and a " +
                "AUX inut for that friend who claims their Soundcloud is fire (sigh).",
                IsFeatured = true,
                IsNew = false,
                IsSold = true,
                ImageUrl = "http://localhost:50822/Images/Corolla.jpg"
            },

            //Stanky
            new VehicleDetails
            {
                VehicleId = 4,
                VIN = "0901DERPXB360LEON",
                ModelName = context.CarModel.FirstOrDefault(n => n.Model=="F-150"),
                Year = 2002,
                Color = "White",
                BodyName = context.BodyStyle.FirstOrDefault(b=>b.BodyType=="Truck"),
                TransmissionType = context.Transmission.FirstOrDefault(t=>t.TransmissionName=="Automatic"),
                Interior = "Gray",
                VehicleType = context.VehicleType.FirstOrDefault(t=>t.TypeDescription=="New"),
                Mileage = 711,
                MSRP = 12000,
                SalePrice = 10000,
                Description = "Everyone needs a friend with a truck because not everyone thinks that they will EVER need to " +
                "move or purchase a new bed or a giant tv they don't need, that is where you come in. This F-150 comes with an extended cab that is sure " +
                "to help all your friends (and your momma too) when the time comes. 2.5 inch lift kit installed with auto-drop bars to climb into your " +
                "mini-monster truck as you ravage through the streets burning your amazing 16MPG with a fat Flowmaster exhaust kit installed.",
                IsFeatured = false,
                IsNew = true,
                IsSold = false,
                ImageUrl = "http://localhost:50822/Images/f150.jpg"
            },

            //Ponch
            new VehicleDetails
            {
                VehicleId = 5,
                VIN = "1218PONCH733GEARS",
                ModelName = context.CarModel.FirstOrDefault(n => n.Model=="F430"),
                Year = 2000,
                Color = "Red",
                BodyName = context.BodyStyle.FirstOrDefault(b=>b.BodyType=="Car"),
                TransmissionType = context.Transmission.FirstOrDefault(t=>t.TransmissionName=="Manual"),
                Interior = "Suede",
                VehicleType = context.VehicleType.FirstOrDefault(v=>v.TypeDescription=="New"),
                Mileage = 430,
                MSRP = 87000,
                SalePrice = 83500,
                Description = "Get ready to feel the pow-a!!! The 4.3L, V8 F430 roars through the streets like a lion " +
                "stalking its prey. Duo-quad pipe exhaust with a riveting 480hp, mid-rear engine mounted for perfect weight distribution to handle the " +
                "sharpest of turns and quick launch to get you 0-60 in just 3.6 seconds.",
                IsFeatured = true,
                IsNew = true,
                IsSold = false,
                ImageUrl = "http://localhost:50822/Images/f430.jpg"
            },

            //Integra
            new VehicleDetails
            {
                VehicleId = 6,
                VIN = "123RANDOM456VIN#",
                ModelName = context.CarModel.FirstOrDefault(n => n.Model=="Integra"),
                Year = 2001,
                Color = "Black",
                BodyName = context.BodyStyle.FirstOrDefault(b=>b.BodyType=="Car"),
                TransmissionType = context.Transmission.FirstOrDefault(t=>t.TransmissionName=="Manual"),
                Interior = "Black",
                VehicleType = context.VehicleType.FirstOrDefault(v=>v.TypeDescription=="Used"),
                Mileage = 3300,
                MSRP = 7500,
                SalePrice = 6700,
                Description = "V-TEC KICKED IN YO!!!! The Integra Type-R at first seems like a small car with not a ton to offer " +
                "beyond its historic name but what you don't know is underhood runs a 1.8L, V4 DOHC V-tec engine powered by Mugen (no pun intended). " +
                "The Japanese import is to much fun to drive and just remember to reve high!",
                IsFeatured = false,
                IsNew = false,
                IsSold = false,
                ImageUrl = "http://localhost:50822/Images/Integra.jpg"
            },

            //HAPPY!!!!!
            new VehicleDetails
            {
                VehicleId = 7,
                VIN = "987RANDO654NUMBER",
                ModelName = context.CarModel.FirstOrDefault(n => n.Model== "Miata MX-5"),
                Year = 2015,
                Color = "Red",
                BodyName = context.BodyStyle.FirstOrDefault(b=>b.BodyType=="Car"),
                TransmissionType = context.Transmission.FirstOrDefault(t=>t.TransmissionName=="Manual"),
                Interior = "Black",
                VehicleType = context.VehicleType.FirstOrDefault(v=>v.TypeDescription=="Used"),
                Mileage = 2300,
                MSRP = 19000,
                SalePrice = 17500,
                Description = "Smile like you mean it! It is always better to drive a slow car fast than it is to " +
                "drive a fast car slow. The Miata MX-5 will be that car you can drive fast and make you smile just the way it smile when you look at it " +
                "from the front. The original Japanese roadster comes with a new 2.0L, I4 engine in its new model with a quick 6 speed manual gearbox and " +
                "a removable hard top for enjoying the sun while coasting in style.",
                IsFeatured = false,
                IsNew = false,
                IsSold = false,
                ImageUrl = "http://localhost:50822/Images/mx5.jpg"
            },

            //Tomcat 
            new VehicleDetails
            {
                VehicleId = 8,
                VIN = "TERRYFREI14886969",
                ModelName = context.CarModel.FirstOrDefault(n => n.Model=="Neon"),
                Year = 2005,
                Color = "Yellow",
                BodyName = context.BodyStyle.FirstOrDefault(b=>b.BodyType=="Car"),
                TransmissionType = context.Transmission.FirstOrDefault(t=>t.TransmissionName=="Manual"),
                Interior = "Black",
                VehicleType = context.VehicleType.FirstOrDefault(v=>v.TypeDescription=="New"),
                Mileage = 722,
                MSRP = 24500,
                SalePrice = 20000,
                Description = "The SRT-4 was meant for speed right out of the box, taking a 2.4L, V4 engine SOHC with a " +
                "turbocharged engine that runs up to 14psi boost and a large intake to keep it cool. The power output let it go 0-60 in 5.3 using " +
                "the downforce from the giant SPOILER ALERT!!! in the back of the car. Grip it and rip it!!",
                IsFeatured = false,
                IsNew = true,
                IsSold = false,
                ImageUrl = "http://localhost:50822/Images/Neon.jpg"
            },

            //Ram
            new VehicleDetails
            {
                VehicleId = 9,
                VIN = "HUGE1500TRUCK11210",
                ModelName = context.CarModel.FirstOrDefault(n => n.Model=="Ram"),
                Year = 2009,
                Color = "Silver",
                BodyName = context.BodyStyle.FirstOrDefault(b=>b.BodyType=="Truck"),
                TransmissionType = context.Transmission.FirstOrDefault(t=>t.TransmissionName=="Automatic"),
                Interior = "Gray",
                VehicleType = context.VehicleType.FirstOrDefault(v=>v.TypeDescription=="Used"),
                Mileage = 19528,
                MSRP = 26000,
                SalePrice = 24000,
                Description = "RAM through your workload in this high powered, extended cab 1500. Honing a powerful 3.7L " +
                "V6 sized engine, you can get up to 6 tons of towing capacity in case you decide someone took your parking spot and want to show them " +
                "the rules of the road or an of your hauling needs.",
                IsFeatured = false,
                IsNew = false,
                IsSold = false,
                ImageUrl = "http://localhost:50822/Images/Ram.jpg"
            });

            context.SaveChanges();

            //purchase
             context.Sale.AddOrUpdate(
                s=>s.SaleId,
                new Sale {
                    SaleId = 1,
                    VehicleId = 4,
                    DateSold = DateTime.Parse("11/25/2017"),
                    PriceSold = 10000,
                    PurchaseType = context.Purchase.FirstOrDefault(p=>p.Type=="Bank Loan"),
                    User = context.Users.First(u=>u.UserName=="Reanna@guildcars.com"),
                    Name = "El Stanko",
                    Address = "123 Random Rd",
                    City = "Sun Diego",
                    State = "CA",
                    Zipcode = 92154,
                    Phone = 619 - 867 - 5309,
                    Email = "derp@yahoo.com"
                }
            );

            context.SaveChanges();

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
