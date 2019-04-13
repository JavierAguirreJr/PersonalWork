using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dealership.Models
{
    public class CarMake
    {
        [Key]
        public int MakeID { get; set; }
        public string Make { get; set; }
        public DateTime DateAdded { get; set; }

        public virtual AppUser User { get; set; }
    }
}
