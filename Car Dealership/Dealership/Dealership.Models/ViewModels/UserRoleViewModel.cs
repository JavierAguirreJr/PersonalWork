using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dealership.Models.ViewModels
{
    public class UserRoleViewModel
    {
        public AppUser AppUser { get; set; }
        public AppRole Role { get; set; }
        public List<SelectListItem> RolesList { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public string RoleId { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public UserRoleViewModel()
        {
            RolesList = new List<SelectListItem>();
        }

        public void SetRoleItems(IEnumerable<IdentityRole> roles)
        {
            foreach (var role in roles)
            {
                RolesList.Add(new SelectListItem()
                {
                    Value = role.Id.ToString(),
                    Text = role.Name
                });
            }
        }
    }
}
