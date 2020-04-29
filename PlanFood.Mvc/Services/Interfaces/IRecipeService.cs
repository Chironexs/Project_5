using PlanFood.Mvc.Models.Db;
using System.Collections.Generic;

namespace PlanFood.Mvc.Services.Interfaces
{
    public interface IRecipeService
    {
        /// <param name="id">User.Id</param>
        /// <returns></returns>
        int CountRecipes(string id);
        bool Create(Recipe recipe);
        bool Delete(int id);
        Recipe Get(int id);
        IList<Recipe> GetAll();
        
        /// <param name="id">User.Id</param>
        /// <returns></returns>
        IList<Recipe> GetAllByUserId(string id);

        bool Update(Recipe recipe);

        IList<Recipe> GetAllOrderByDateDescending();
        IList<Recipe> GetAllWithName(string name);
    }
}