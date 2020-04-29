using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace PlanFood.Mvc.Models.Db
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Recipe> Recipes { get; set; }
    }
}