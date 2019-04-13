using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dealership.Models
{
    public class Sale
    {
        [Key]
        public int SaleId { get; set; }
        [Required]
        public string Name { get; set; }
        public int Phone { get; set; }
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public int Zipcode { get; set; }
        public int VehicleId { get; set; }
        [Required]
        public decimal PriceSold { get; set; }
        public DateTime DateSold { get; set; }
        public bool IsSold { get; set; }
        public bool IsFeatured { get; set; }

        public virtual PurchaseType PurchaseType { get; set; }
        public virtual AppUser User { get; set; }
        public virtual VehicleDetails Vehicle { get; set; }

    }
}
