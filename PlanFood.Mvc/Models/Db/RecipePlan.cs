using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlanFood.Mvc.Models.Db
{
    public class RecipePlan
    {
        public int Id { get; set; }

        [ForeignKey("DayName")]
        public int DayNameId { get; set; }
        public DayName DayName { get; set; }

        public int DisplayOrder { get; set; }

        [Required, StringLength(245)]
        public string MealName { get; set; }             
        
        [ForeignKey("Plan")]
        public int PlanId { get; set; }
        public Plan Plan { get; set; }

        [ForeignKey("Recipe")]
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }

    }
}