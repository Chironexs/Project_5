using PlanFood.Mvc.Models.Db;
using System.Collections.Generic;

namespace PlanFood.Mvc.Services.Interfaces
{
	public interface IBookService
	{
		bool Create(Book book);
		bool Delete(int id);
		Book Get(int id);
		IList<Book> GetAll();
		bool Update(Book book);
	}
}