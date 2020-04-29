using PlanFood.Mvc.Models.Db;
using System.Collections.Generic;

namespace PlanFood.Mvc.Services.Interfaces
{
    public interface IUserService
    {
        IList<User> GetAll();
        User GetById(string id);
        bool Update(User user);
    }
}
