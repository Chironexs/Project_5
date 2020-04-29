using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PlanFood.Mvc.Models.Db
{
    public class DayName
    {
        public int Id { get; set; }
        public int DisplayOrder { get; set; }
        [Required, StringLength(45)]
        public string Name { get; set; }

        public ICollection<RecipePlan> RecipePlans { get; set; }
    }
}