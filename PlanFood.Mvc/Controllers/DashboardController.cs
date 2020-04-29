using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PlanFood.Mvc.Models.Db;
using PlanFood.Mvc.Models.ViewModel;
using PlanFood.Mvc.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlanFood.Mvc.Controllers
{
    [Authorize(Roles = "ActiveUser")]
    public class DashboardController : Controller
    {
        private readonly IPlanService planService;
        private readonly IRecipePlanService recipePlanService;
        private readonly IRecipeService recipeService;
        private readonly IUserService userService;
        private readonly UserManager<IdentityUser> userManager;

        public DashboardController(IPlanService _planService,
            IRecipePlanService _recipePlanService, 
            IRecipeService _recipeService,
            IUserService _userService,
            UserManager<IdentityUser> _userManager)
        {
            planService = _planService;
            recipePlanService = _recipePlanService;
            recipeService = _recipeService;
            userService = _userService;
            userManager = _userManager;
        }
        public IActionResult Index()
        {
            DashboardViewModel dashboardViewModel = new DashboardViewModel
            {
                RecipeCount = recipeService.CountRecipes(userManager.GetUserId(User)),
                PlanCount = planService.CountPlans(userManager.GetUserId(User)),
                Plan = planService.LastPlanDetails(userManager.GetUserId(User))
            };

            if (dashboardViewModel.Plan == null)
            {
                return View(dashboardViewModel);
            }
            
            dashboardViewModel.DayNames = new List<DayName>();
            dashboardViewModel.RecipePlans = recipePlanService.GetAllByPlanIdDisplayOrderAscending(dashboardViewModel.Plan.Id);

            foreach(var recipePlan in dashboardViewModel.RecipePlans)
            {
                if (!dashboardViewModel.DayNames.Contains(recipePlan.DayName))
                {
                    dashboardViewModel.DayNames.Add(recipePlan.DayName);
                }
            }

            return View(dashboardViewModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Users()
        {
            return View(userService.GetAll());
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Block(string id)
        {
            await userManager.RemoveFromRoleAsync(await userManager.FindByIdAsync(id), "ActiveUser");

            return RedirectToAction("Users");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Unlock(string id)
        {
            await userManager.AddToRoleAsync(await userManager.FindByIdAsync(id), "ActiveUser");

            return RedirectToAction("Users");
        }

        [HttpGet]
        public IActionResult Password()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Password(PasswordViewModel passwordViewModel)
        {
            if(ModelState.IsValid)
            {
                IdentityUser identityUser = userManager.FindByIdAsync(userManager.GetUserId(HttpContext.User)).Result;

                var result = userManager.RemovePasswordAsync(identityUser).Result;

                if (result.Succeeded)
                {
                    result = userManager.AddPasswordAsync(identityUser, passwordViewModel.Password).Result;

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }

                    ModelState.AddModelError("", "Nie można zmienić hasła!");
                }
                else
                {
                    ModelState.AddModelError("", "Nie można zmienić hasła!");
                }
            }
            
            return View(passwordViewModel);
        }

        [HttpGet]
        public IActionResult UserData()
        {
            User user = userService.GetById(userManager.GetUserId(HttpContext.User));

            UserDataViewModel userDataViewModel = new UserDataViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };

            return View(userDataViewModel);
        }

        [HttpPost]
        public IActionResult UserData(UserDataViewModel userDataViewModel)
        {
            if(ModelState.IsValid)
            {
                User user = userService.GetById(userManager.GetUserId(HttpContext.User));

                user.UserName = userDataViewModel.UserName;
                user.FirstName = userDataViewModel.FirstName;
                user.LastName = userDataViewModel.LastName;
                user.Email = userDataViewModel.Email;
                user.NormalizedEmail = userDataViewModel.Email.ToUpper();
                user.NormalizedUserName = userDataViewModel.Email.ToUpper();

                userService.Update(user);

                return RedirectToAction("Index");
            }

            return View(userDataViewModel);
        }
    }
}