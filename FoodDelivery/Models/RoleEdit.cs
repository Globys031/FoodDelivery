using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace FoodDelivery.Models
{
    // The RoleEdit class is used to represent the Role and the details of the Users who are in the role or not in the role.
    public class RoleEdit
    {
        public IdentityRole Role { get; set; }
        public IEnumerable<IdentityUser> Members { get; set; }
        public IEnumerable<IdentityUser> NonMembers { get; set; }
    }
}