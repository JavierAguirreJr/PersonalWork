using Dealership.Data.Interface;
using Dealership.Data.Repos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dealership.Data
{
    public class CarDealerFactory
    {
        public static IDealerRepo Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();

            switch (mode)
            {
                case "Prod":
                    return new EFRepo();
                case "QA":
                    return new TestRepo();
                default:
                    throw new Exception("Mode value in app config is not valid");
            }
        }
    }
}
