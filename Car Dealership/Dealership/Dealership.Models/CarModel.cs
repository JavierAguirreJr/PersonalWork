using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dealership.Models
{
    public class CarModel
    {
        [Key]
        public int ModelID { get; set; }
        public string Model { get; set; }
        public DateTime DateAdded { get; set; }

        public virtual CarMake MakeName { get; set; }
        public virtual AppUser User { get; set; }

    }
}
