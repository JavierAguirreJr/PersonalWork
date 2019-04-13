using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dealership.Models
{
    public class CarDealershipDbContext : IdentityDbContext<AppUser>
    {
        public CarDealershipDbContext() : base("CarDealership")
        {

        }

        public DbSet<PurchaseType> Purchase { get; set; }
        public DbSet<ContactUs> Contact { get; set; }
        public DbSet<CarMake> CarMake { get; set; }
        public DbSet<CarModel> CarModel { get; set; }
        public DbSet<Transmission>Transmission { get; set; }
        public DbSet<VehicleType>VehicleType { get; set; }
        public DbSet<VehicleDetails> VehicleDetails { get; set; }
        public DbSet<BodyStyle> BodyStyle { get; set; }
        public DbSet<Specials> Specials { get; set; }
        public DbSet<Sale> Sale { get; set; }
        public DbSet<CustomerInformation> CustomerInformation { get; set; }
    }
}
