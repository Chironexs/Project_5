using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PlanFood.Mvc.Models.Db;
using PlanFood.Mvc.Services.Interfaces;

namespace PlanFood.Mvc.Controllers
{
    [Authorize(Roles = "ActiveUser")]
    public class RecipeController : Controller
    {
        private readonly IRecipePlanService recipePlanService;
        private readonly IRecipeService recipeService;
        private readonly UserManager<IdentityUser> userManager;

        public RecipeController(IRecipePlanService _recipePlanService,
            IRecipeService _recipeService, 
            UserManager<IdentityUser> _userManager)
        {
            recipePlanService = _recipePlanService;
            recipeService = _recipeService;
            userManager = _userManager;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                recipe.UserId = userManager.GetUserId(HttpContext.User);

                recipeService.Create(recipe);
            }

            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult List()
        {
            return View(recipeService.GetAllByUserId(userManager.GetUserId(HttpContext.User)));
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            return View(recipeService.Get(id));
        }

        [HttpGet]
        public IActionResult Remove(int id)
        {
            if(recipePlanService.CountOfPlanWithTheRecipe(id) == 0)
            {
                return View(recipeService.Get(id));
            }
            return RedirectToAction("List");
        }

        [HttpPost]
        public IActionResult ConfirmRemove(int id)
        {
            recipeService.Delete(id);

            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(recipeService.Get(id));
        }

        [HttpPost]
        public IActionResult Edit(Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                recipe.UserId = userManager.GetUserId(HttpContext.User);
                recipeService.Update(recipe);
            }
            return RedirectToAction("List");
        }
    }
}