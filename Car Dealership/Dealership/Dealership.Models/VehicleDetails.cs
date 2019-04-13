using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dealership.Models
{
    public class VehicleDetails
    {
        [Key]
        public int VehicleId { get; set; }
        [Required]
        public string VIN { get; set; }
        [Range(2000,2018)]
        public int Year { get; set; }
        public int Mileage { get; set; }
        [Required]
        public string Color { get; set; }
        [Required]
        public string Interior { get; set; }
        [Required]
        public decimal SalePrice { get; set; }
        [Required]
        public decimal MSRP { get; set; }
        [Required]
        public string Description { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsNew { get; set; }
        public string ImageUrl { get; set; }
        public bool IsSold { get; set; }
        public byte[] Image { get; set; }

        [Required]
        public virtual CarModel ModelName { get; set; }
        public virtual BodyStyle BodyName { get; set; }
        public virtual Transmission TransmissionType { get; set; }
        public virtual VehicleType VehicleType { get; set; }
        public virtual AppUser User { get; set; }
    }
}
