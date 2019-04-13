using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dealership.Models
{
    public class PurchaseType
    {
        //cash, bank loan or dealer credit/loan
        public int PurchaseTypeId { get; set; }
        public string Type { get; set; }
    }
}
