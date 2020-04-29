using Microsoft.AspNetCore.Mvc;
using PlanFood.Mvc.Services.Interfaces;

namespace PlanFood.Mvc.Controllers
{
	public class BookController : Controller
	{
		private readonly IBookService bookService;

		public BookController(IBookService _bookService)
		{
			bookService = _bookService;
		}

		public IActionResult Index()
		{
			var books = bookService.GetAll();
			return View(books);
		}
	}
}