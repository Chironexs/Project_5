using Microsoft.EntityFrameworkCore;
using PlanFood.Mvc.Context;
using PlanFood.Mvc.Models.Db;
using PlanFood.Mvc.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlanFood.Mvc.Services
{
    public class PlanService : IPlanService
    {
        private readonly PlanFoodContext context;

        public PlanService(PlanFoodContext _context)
        {
            context = _context;
        }

        public int CountPlans(string id)
        {
            return context.Plans.Where(a => a.UserId == id).Count();
        }

        public bool Create(Plan plan)
        {
            plan.Created = DateTime.UtcNow;

            context.Plans.Add(plan);

            return context.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var plan = context.Plans.SingleOrDefault(a => a.Id == id);

            if (plan == null)
            {
                return false;
            }

            context.Plans.Remove(plan);

            return context.SaveChanges() > 0;
        }

        public Plan Get(int id)
        {
            return context.Plans.SingleOrDefault(a => a.Id == id);
        }

        public IList<Plan> GetAll()
        {
            return context.Plans.ToList();
        }

        public IList<Plan> GetAllByUserId(string id)
        {
            return context.Plans.Where(a => a.UserId == id).ToList();
        }

        public Plan LastPlanDetails(string id)
        {
            return context.Plans
                .Include(a => a.RecipePlans)
                .ThenInclude(a => a.DayName)
                .Include(a => a.RecipePlans)
                .ThenInclude(a => a.Recipe)
                .Where(a => a.UserId == id)
                .OrderByDescending(a => a.Created)
                .FirstOrDefault();
        }

        public Plan PlanDetails(int id)
        {
            return context.Plans
                .Include(a => a.RecipePlans)
                .ThenInclude(a => a.DayName)
                .Include(a => a.RecipePlans)
                .ThenInclude(a => a.Recipe)
                .SingleOrDefault(a => a.Id == id);
        }

        public bool Update(Plan plan)
        {
            context.Plans.Update(plan);

            context.Entry<Plan>(plan).Property(a => a.Created).IsModified = false;
            return context.SaveChanges() > 0;
        }
    }
}