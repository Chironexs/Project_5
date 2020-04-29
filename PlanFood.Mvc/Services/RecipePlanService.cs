using PlanFood.Mvc.Context;
using PlanFood.Mvc.Models.Db;
using PlanFood.Mvc.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace PlanFood.Mvc.Services
{
    public class RecipePlanService : IRecipePlanService
    {
        private readonly PlanFoodContext context;

        public RecipePlanService(PlanFoodContext _context)
        {
            context = _context;
        }

        public int CountOfPlanWithTheRecipe(int id)
        {
            return context.RecipePlans.Where(a => a.RecipeId == id).Count();
        }

        public bool Create(RecipePlan recipePlan)
        {
            context.RecipePlans.Add(recipePlan);
            
            return context.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var recipePlan = context.RecipePlans.SingleOrDefault(a => a.Id == id);
            
            if (recipePlan == null)
            {
                return false;
            }

            context.RecipePlans.Remove(recipePlan);
            
            return context.SaveChanges() > 0;
        }

        public bool DeleteAllWithPlanId(int id)
        {
            var recipePlans = context.RecipePlans.Where(a => a.PlanId == id);
            
            if (recipePlans == null)
            {
                return false;
            }

            foreach(var recipePlan in recipePlans)
            {
                context.RecipePlans.Remove(recipePlan);
            }
            
            return context.SaveChanges() > 0;
        }

        public RecipePlan Get(int id)
        {
            return context.RecipePlans.SingleOrDefault(a => a.Id == id);
        }

        public IList<RecipePlan> GetAll()
        {
            return context.RecipePlans.ToList();
        }

        public IList<RecipePlan> GetAllByPlanIdDisplayOrderAscending(int id)
        {
            return context.RecipePlans
                .Where(a => a.PlanId == id)
                .OrderBy(a => a.DisplayOrder)
                .ToList();
        }

        public bool Update(RecipePlan recipePlan)
        {
            context.RecipePlans.Update(recipePlan);
            
            return context.SaveChanges() > 0;
        }
    }
}
