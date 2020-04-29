using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PlanFood.Mvc.Models.Db;
using PlanFood.Mvc.Models.ViewModel;
using PlanFood.Mvc.Services.Interfaces;
using System.Collections.Generic;

namespace PlanFood.Mvc.Controllers
{
    [Authorize(Roles = "ActiveUser")]
    public class PlanController : Controller
    {
        private readonly IDayNameService dayNameService;
        private readonly IPlanService planService;
        private readonly IRecipeService recipeService;
        private readonly IRecipePlanService recipePlanService;
        private readonly UserManager<IdentityUser> userManager;

        public PlanController(IDayNameService _dayNameService,
            IPlanService _planService,
            IRecipeService _recipeService,
            IRecipePlanService _recipePlanService,
            UserManager<IdentityUser> _userManager)
        {
            dayNameService = _dayNameService;
            planService = _planService;
            recipeService = _recipeService;
            recipePlanService = _recipePlanService;
            userManager = _userManager;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Plan plan)
        {
            if (ModelState.IsValid)
            {
                plan.UserId = userManager.GetUserId(HttpContext.User);

                planService.Create(plan);
            }

            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult AddRecipe()
        {
            RecipePlanViewModel recipePlanViewModel = new RecipePlanViewModel
            {
                DayNames = dayNameService.GetAll(),
                Recipes = recipeService.GetAllByUserId(userManager.GetUserId(HttpContext.User)),
                Plans = planService.GetAllByUserId(userManager.GetUserId(HttpContext.User))
            };

            return View(recipePlanViewModel);
        }

        [HttpPost]
        public IActionResult AddRecipe(RecipePlan recipePlan)
        {
            if (ModelState.IsValid)
            {
                recipePlanService.Create(recipePlan);
            }

            return RedirectToAction("AddRecipe");
        }

        [HttpGet]
        public IActionResult List()
        {
            return View(planService.GetAllByUserId(userManager.GetUserId(HttpContext.User)));
        }

        [HttpGet]
        public IActionResult Remove(int id)
        {
            return View(planService.Get(id));
        }

        public IActionResult ConfirmRemove(int id)
        {
            recipePlanService.DeleteAllWithPlanId(id);
            planService.Delete(id);

            return RedirectToAction("List");
        }

        /// <param name="id">RecipePlan.Id</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult RemoveRecipe(int id)
        {
            RecipePlan recipePlan = recipePlanService.Get(id);

            recipePlan.Plan = planService.Get(recipePlan.PlanId);
            recipePlan.Recipe = recipeService.Get(recipePlan.RecipeId);

            return View(recipePlan);
        }

        /// <param name="id">RecipePlan.Id</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ConfirmRemoveRecipe(int id)
        {
            recipePlanService.Delete(id);

            return RedirectToAction("Details");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            PlanDetailsViewModel planDetailsViewModel = new PlanDetailsViewModel()
            {
                Plan = planService.PlanDetails(id)
            };

            planDetailsViewModel.DayNames = new List<DayName>();
            planDetailsViewModel.RecipePlans = recipePlanService.GetAllByPlanIdDisplayOrderAscending(planDetailsViewModel.Plan.Id);

            foreach (var recipePlan in planDetailsViewModel.RecipePlans)
            {
                if (!planDetailsViewModel.DayNames.Contains(recipePlan.DayName))
                {
                    planDetailsViewModel.DayNames.Add(recipePlan.DayName);
                }
            }

            return View(planDetailsViewModel);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(planService.Get(id));
        }

        [HttpPost]
        public IActionResult Edit(Plan plan)
        {
            if (ModelState.IsValid)
            {
                plan.UserId = userManager.GetUserId(HttpContext.User);
                planService.Update(plan);
            }
            return RedirectToAction("List");
        }
    }
}