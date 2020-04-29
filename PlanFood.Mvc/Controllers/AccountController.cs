using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PlanFood.Mvc.Models.Db;
using PlanFood.Mvc.Models.ViewModel;
using System.Threading.Tasks;

namespace PlanFood.Mvc.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountController(SignInManager<IdentityUser> _signInManager,
            UserManager<IdentityUser> _userManager,
            RoleManager<IdentityRole> _roleManager)
        {
            signInManager = _signInManager;
            userManager = _userManager;
            roleManager = _roleManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            if (ModelState.IsValid)
            {
                var user = new User()
                {
                    UserName = register.Email,
                    Email = register.Email,
                    FirstName = register.FirstName,
                    LastName = register.LastName,
                };

                if (!await roleManager.RoleExistsAsync("ActiveUser")) { await roleManager.CreateAsync(new IdentityRole("ActiveUser")); }
                if (!await roleManager.RoleExistsAsync("Admin")) { await roleManager.CreateAsync(new IdentityRole("Admin")); }

                var result = await userManager.CreateAsync(user, register.Password);
                
                if (result.Succeeded)
                {
                    var login = await signInManager.PasswordSignInAsync(register.Login, register.Password, true, false);

                    if (login.Succeeded)
                    {
                        await userManager.AddToRoleAsync(await userManager.FindByNameAsync(register.Login), "ActiveUser");

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Nie można się zalogować!");
                    }
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(register);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LogInViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(viewModel.UserName, viewModel.Password, true, false);

                var isInRole = await userManager.IsInRoleAsync(
                    await userManager.FindByNameAsync(
                        viewModel.UserName), "ActiveUser");
                
                if (result.Succeeded && isInRole)
                {
                    var a = userManager.GetUserId(HttpContext.User);
                    return RedirectToAction("Index", "Dashboard");
                }
                if(result.Succeeded && !isInRole)
                {
                    ModelState.AddModelError("", "Jesteś zablokowany!");
                }
                else
                {
                    ModelState.AddModelError("", "Nie można się zalogować!");
                }
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}