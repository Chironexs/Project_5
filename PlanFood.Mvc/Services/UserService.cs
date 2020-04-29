using PlanFood.Mvc.Context;
using PlanFood.Mvc.Models.Db;
using PlanFood.Mvc.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace PlanFood.Mvc.Services
{
    public class UserService : IUserService
    {
        private readonly PlanFoodContext context;

        public UserService(PlanFoodContext _context)
        {
            context = _context;
        }

        public IList<User> GetAll()
        {
            return context.Users.ToList();
        }

        public User GetById(string id)
        { 
            return context.Users.SingleOrDefault(a => a.Id == id);
        }

        public bool Update(User user)
        {
            context.Users.Update(user);

            return context.SaveChanges() > 0;
        }
    }
}
