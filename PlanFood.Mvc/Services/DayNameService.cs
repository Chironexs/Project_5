using PlanFood.Mvc.Context;
using PlanFood.Mvc.Models.Db;
using PlanFood.Mvc.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace PlanFood.Mvc.Services
{
    public class DayNameService : IDayNameService
    {
        private readonly PlanFoodContext context;

        public DayNameService(PlanFoodContext _context)
        {
            context = _context;
        }

        public bool Create(DayName dayName)
        {
            context.DayNames.Add(dayName);
            
            return context.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var dayName = context.DayNames.SingleOrDefault(a => a.Id == id);
            
            if (dayName == null)
            {
                return false;
            }

            context.DayNames.Remove(dayName);
            
            return context.SaveChanges() > 0;
        }

        public DayName Get(int id)
        {
            return context.DayNames.SingleOrDefault(a => a.Id == id);
        }

        public IList<DayName> GetAll()
        {
            return context.DayNames.ToList();
        }

        public bool Update(DayName dayName)
        {
            context.DayNames.Update(dayName);
            
            return context.SaveChanges() > 0;
        }
    }
}
