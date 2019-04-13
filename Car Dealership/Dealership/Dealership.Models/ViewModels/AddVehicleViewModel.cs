using Dealership.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Dealership.Models.ViewModels
{
    public class AddVehicleViewModel
    {
        public VehicleDetails Vehicle { get; set; }
        public List<SelectListItem> MakeType { get; set; }
        public List<SelectListItem> ModelType { get; set; }
        public List<SelectListItem> VehicleType { get; set; }
        public List<SelectListItem> BodyType { get; set; }
        public List<SelectListItem> TransmissionType { get; set; }
        public List<SelectListItem> ColorOption { get; set; }
        public List<SelectListItem> InteriorType { get; set; }
        public HttpPostedFileBase VehicleImage { get; set; }

        public AddVehicleViewModel()
        {
            Vehicle = new VehicleDetails();
            MakeType = new List<SelectListItem>();
            ModelType = new List<SelectListItem>();
            VehicleType = new List<SelectListItem>();
            BodyType = new List<SelectListItem>();
            TransmissionType = new List<SelectListItem>();
            ColorOption = new List<SelectListItem>();
            InteriorType = new List<SelectListItem>();
        }

        public void SetMakeType(IEnumerable<CarMake> make)
        {
            foreach (var makeType in make)
            {
                MakeType.Add(new SelectListItem()
                {
                    Value = makeType.MakeID.ToString(),
                    Text = makeType.Make
                });
            }
        }

        public void SetModelType(IEnumerable<CarModel> model)
        {
            foreach (var modelType in model)
            {
                ModelType.Add(new SelectListItem()
                {
                    Value = modelType.ModelID.ToString(),
                    Text = modelType.Model
                });
            }
        }

        public void SetVehicleType(IEnumerable<VehicleType> vehicleType)
        {
            foreach (var condition in vehicleType)
            {
                VehicleType.Add(new SelectListItem()
                {
                    Value = condition.VehicleTypeId.ToString(),
                    Text = condition.TypeDescription
                });
            }
        }

        public void SetBodyType(IEnumerable<BodyStyle> bodyStyle)
        {
            foreach (var bodyType in bodyStyle)
            {
                BodyType.Add(new SelectListItem()
                {
                    Value = bodyType.BodyStyleId.ToString(),
                    Text = bodyType.BodyType
                });
            }
        }

        public void SetTransmissionType(IEnumerable<Transmission> trannyType)
        {
            foreach (var tranny in trannyType)
            {
                TransmissionType.Add(new SelectListItem()
                {
                    Value = tranny.TransmissionId.ToString(),
                    Text = tranny.TransmissionName
                });
            }
        }

        public void SetColorOptions()
        {
            //white, blue, red, silver, orange

            ColorOption.Add(new SelectListItem()
            {
                Value = "White",
                Text = "White"

            });

            ColorOption.Add(new SelectListItem()
            {
                Value = "Blue",
                Text = "Blue"
            });

            ColorOption.Add(new SelectListItem()
            {
                Value = "Red",
                Text = "Red"
            });

            ColorOption.Add(new SelectListItem()
            {
                Value = "Silver",
                Text = "Silver"
            });

            ColorOption.Add(new SelectListItem()
            {
                Value = "Orange",
                Text = "Orange"
            });
            ColorOption.Add(new SelectListItem()
            {
                Value = "Black",
                Text = "Black"
            });
            ColorOption.Add(new SelectListItem()
            {
                Value = "Yellow",
                Text = "Yellow"
            });
        }

        public void SetInteriorTypes()
        {
            //black, black leather, beige, white, white/gold
            InteriorType.Add(new SelectListItem()
            {
                Value = "Black",
                Text = "Black"
            });

            InteriorType.Add(new SelectListItem()
            {
                Value = "Black Leather",
                Text = "Black Leather"
            });

            InteriorType.Add(new SelectListItem()
            {
                Value = "Beige",
                Text = "Beige"
            });

            InteriorType.Add(new SelectListItem()
            {
                Value = "White",
                Text = "White"
            });

            InteriorType.Add(new SelectListItem()
            {
                Value = "White/Gold",
                Text = "White/Gold"
            });
            InteriorType.Add(new SelectListItem()
            {
                Value = "Suede",
                Text = "Suede"
            });
            InteriorType.Add(new SelectListItem()
            {
                Value = "Gray",
                Text = "Gray"
            });
        }
    }
}
