using PlanFood.Mvc.Models.Db;
using System.Collections.Generic;

namespace PlanFood.Mvc.Models.ViewModel
{
    public class DashboardViewModel
    {
        public int RecipeCount { get; set; }
        public int PlanCount { get; set; }
        public Plan Plan { get; set; }
        public ICollection<DayName> DayNames { get; set; }
        public ICollection<RecipePlan> RecipePlans { get; set; }
    }
}
