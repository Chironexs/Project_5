using PlanFood.Mvc.Models.Db;
using System.Collections.Generic;

namespace PlanFood.Mvc.Services.Interfaces
{
    public interface IPlanService
    {
        /// <param name="id">User.Id</param>
        int CountPlans(string id);
        bool Create(Plan plan);
        bool Delete(int id);
        Plan Get(int id);
        IList<Plan> GetAll();
        IList<Plan> GetAllByUserId(string id);

        /// <param name="id">User.Id</param>
        Plan LastPlanDetails(string id);

        /// <param name="id">Plan.Id</param>
        Plan PlanDetails(int id);
        bool Update(Plan plan);
    }
}
