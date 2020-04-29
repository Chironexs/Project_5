using PlanFood.Mvc.Context;
using PlanFood.Mvc.Models.Db;
using PlanFood.Mvc.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace PlanFood.Mvc.Services
{
	public class BookService : IBookService
	{
		private readonly PlanFoodContext context;

		public BookService(PlanFoodContext _context)
		{
			context = _context;
		}

		public bool Create(Book book)
		{
			context.Books.Add(book);
			
			return context.SaveChanges() > 0;
		}

		public bool Delete(int id)
		{
			var book = context.Books.SingleOrDefault(a => a.Id == id);

			if (book == null)
			{
				return false;
			}

			context.Books.Remove(book);
			
			return context.SaveChanges() > 0;
		}

		public Book Get(int id)
		{
			return context.Books.SingleOrDefault(a => a.Id == id);
		}

		public IList<Book> GetAll()
		{
			return context.Books.ToList();
		}

		public bool Update(Book book)
		{
			context.Books.Update(book);
			
			return context.SaveChanges() > 0;
		}
	}
}
