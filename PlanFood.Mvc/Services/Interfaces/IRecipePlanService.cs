using PlanFood.Mvc.Models.Db;
using System.Collections.Generic;

namespace PlanFood.Mvc.Services.Interfaces
{
    public interface IRecipePlanService
    {
        int CountOfPlanWithTheRecipe(int id);
        bool Create(RecipePlan recipePlan);
        bool Delete(int id);

        /// <param name="id">Plan.Id</param>
        /// <returns></returns>
        bool DeleteAllWithPlanId(int id);
        RecipePlan Get(int id);
        IList<RecipePlan> GetAll();

        /// <param name="id">Plan.Id</param>
        /// <returns></returns>
        IList<RecipePlan> GetAllByPlanIdDisplayOrderAscending(int id);
        bool Update(RecipePlan recipePlan);
    }
}
