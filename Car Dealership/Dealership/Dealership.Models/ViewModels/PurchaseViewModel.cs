using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dealership.Models.ViewModels
{
    public class PurchaseViewModel
    {
        public VehicleDetails Vehicle { get; set; }
        public Sale SaleDetails { get; set; }
        public PurchaseType PurchaseTypeDetails { get; set; }
        public List<SelectListItem> PurchaseTypes { get; set; }
        public int VehiclePurchaseId { get; set; }

        public PurchaseViewModel()
        {
            PurchaseTypes = new List<SelectListItem>();
        }

        public void SetPurchaseTypes(IEnumerable<PurchaseType> purchaseTypes)
        {
            foreach (var purchaseType in purchaseTypes)
            {
                PurchaseTypes.Add(new SelectListItem()
                {
                    Value = purchaseType.PurchaseTypeId.ToString(),
                    Text = purchaseType.Type
                });
            }
        }
    }
}
