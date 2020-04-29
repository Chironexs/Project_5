using PlanFood.Mvc.Context;
using PlanFood.Mvc.Models.Db;
using PlanFood.Mvc.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlanFood.Mvc.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly PlanFoodContext context;

        public RecipeService(PlanFoodContext _context)
        {
            context = _context;
        }

        public int CountRecipes(string id)
        {
            return context.Recipes.Where(a => a.UserId == id).Count();
        }

        public bool Create(Recipe recipe)
        {
            recipe.Created = DateTime.UtcNow;

            context.Recipes.Add(recipe);
            
            return context.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var recipe = context.Recipes.SingleOrDefault(a => a.Id == id);

            if (recipe == null)
            {
                return false;
            }

            context.Recipes.Remove(recipe);

            return context.SaveChanges() > 0;
        }

        public Recipe Get(int id)
        {
            return context.Recipes.SingleOrDefault(a => a.Id == id);
        }

        public IList<Recipe> GetAll()
        {
            return context.Recipes.ToList();
        }

        public IList<Recipe> GetAllByUserId(string id)
        {
            return context.Recipes.Where(a => a.UserId == id).ToList();
        }

        public bool Update(Recipe recipe)
        {
            recipe.Updated = DateTime.UtcNow;

            context.Recipes.Update(recipe);

            context.Entry<Recipe>(recipe).Property(a => a.Created).IsModified = false;

            return context.SaveChanges() > 0;
        }

        public IList<Recipe> GetAllOrderByDateDescending()
        {
            return context.Recipes.OrderByDescending(a => a.Created).ToList();
        }

        public IList<Recipe> GetAllWithName(string name)
        {
            return context.Recipes.Where(a => a.Name.Contains(name)).ToList();
        }
    }
}
