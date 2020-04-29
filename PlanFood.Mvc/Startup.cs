using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PlanFood.Mvc.Context;
using PlanFood.Mvc.Services;
using PlanFood.Mvc.Services.Interfaces;

namespace PlanFood.Mvc
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<PlanFoodContext>(builer => builer.UseSqlServer(@"Data Source=.\SQLEXPRESS;Initial Catalog=PlanFood;Integrated Security=True"));
			
			services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<PlanFoodContext>();

			services.AddScoped<IBookService, BookService>();

			services.AddScoped<IDayNameService, DayNameService>();
			
			services.AddScoped<IPlanService, PlanService>();
			
			services.AddScoped<IRecipePlanService, RecipePlanService>();
			
			services.AddScoped<IRecipeService, RecipeService>();

			services.AddScoped<IUserService, UserService>();

			services.AddMvc();
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseBrowserLink();

				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseStaticFiles();

			app.UseAuthentication();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
