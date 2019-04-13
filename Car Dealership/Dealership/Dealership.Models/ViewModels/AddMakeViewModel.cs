using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dealership.Models.ViewModels
{
    public class AddMakeViewModel
    {
        public CarMake MakeName { get; set; }

        [Required]
        public string Name { get; set; }
        public IEnumerable<CarMake> MakeList { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
