using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PlanFood.Mvc.Models.Db
{
    public class Recipe
    {
        public int Id { get; set; }

        public DateTime Created { get; set; }

        public string Description { get; set; }

        [Required]
        public string Ingredients { get; set; }

        [Required, StringLength(255)]
        public string Name { get; set; }

        [Required]
        public string Preparation { get; set; }

        [Required]
        public int PreparationTime { get; set; }

        public DateTime? Updated { get; set; }

        [StringLength(450)]
        public string UserId { get; set; }
        public User User { get; set; }

        public ICollection<RecipePlan> RecipePlans { get; set; }
    }
}