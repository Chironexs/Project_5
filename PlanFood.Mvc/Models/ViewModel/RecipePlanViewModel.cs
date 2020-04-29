using PlanFood.Mvc.Models.Db;
using System.Collections.Generic;

namespace PlanFood.Mvc.Models.ViewModel
{
    public class RecipePlanViewModel : RecipePlan
    {
        public ICollection<DayName> DayNames { get; set; }
        public ICollection<Plan> Plans { get; set; }
        public ICollection<Recipe> Recipes { get; set; }
    }
}
