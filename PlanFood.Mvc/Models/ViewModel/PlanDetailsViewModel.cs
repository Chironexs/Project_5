using PlanFood.Mvc.Models.Db;
using System.Collections.Generic;

namespace PlanFood.Mvc.Models.ViewModel
{
    public class PlanDetailsViewModel
    {
        public Plan Plan { get; set; }
        public ICollection<DayName> DayNames { get; set; }
        public ICollection<RecipePlan> RecipePlans { get; set; }
    }
}
