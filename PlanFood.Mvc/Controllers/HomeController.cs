using Microsoft.AspNetCore.Mvc;
using PlanFood.Mvc.Models.Db;
using PlanFood.Mvc.Models.ViewModel;
using PlanFood.Mvc.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace PlanFood.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRecipeService recipeService;

        public HomeController(IRecipeService _recipeService)
        {
            recipeService = _recipeService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RecipeList()
        {
            return View(recipeService.GetAllOrderByDateDescending());
        }

        [HttpGet]
        public IActionResult RecipeDetails(int id)
        {
            return View(recipeService.Get(id));
        }

        [HttpPost]
        public IActionResult SearchRecipe(HomeViewModel homeViewModel)
        {
            ICollection<Recipe> recipes = recipeService.GetAllWithName(homeViewModel.RecipeName);

            if (recipes.Count == 1)
            {
                return View("RecipeDetails", recipes.SingleOrDefault());
            }
            else if(recipes.Count > 1)
            {
                return View($"RecipeList", recipes);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Contact(ContactViewModel model)
        {    
            model.CompanyName = "Zaplanuj Jedzonko";
            model.CompanyAddress = "ul. Krakowskie Przedmieście 51 ";
            model.PostalCodeAndCity = "00-068 Warszawa";
            model.CompanyPhoneNumber = "tel. (22) 320-02-00";
            model.CompanyEmailAddress = "e-mail: zaplanujjedzonko@o2.pl";

            return View(model);
        }

        public IActionResult About(AboutViewModel model)
        {
            model.Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";

            return View(model);
        }
    }
}