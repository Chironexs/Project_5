using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PlanFood.Mvc.Models.Db
{
    public class Plan
    {
        public int Id { get; set; }

        public DateTime Created { get; set; }

        public string Description { get; set; }

        [Required, StringLength(45)]
        public string Name { get; set; }

        [StringLength(450)]
        public string UserId { get; set; }
        public User User { get; set; }

        public ICollection<RecipePlan> RecipePlans { get; set; }
    }
}