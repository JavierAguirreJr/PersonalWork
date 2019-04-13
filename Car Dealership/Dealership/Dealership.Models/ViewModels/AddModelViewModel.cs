using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dealership.Models.ViewModels
{
    public class AddModelViewModel
    {
        public CarModel ModelName { get; set; }

        [Required]
        public string Name { get; set; }
        public IEnumerable<CarModel> ModelList { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
