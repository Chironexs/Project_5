using PlanFood.Mvc.Models.Db;
using System.Collections.Generic;

namespace PlanFood.Mvc.Services.Interfaces
{
    public interface IDayNameService
    {
        bool Create(DayName dayName);
        bool Delete(int id);
        DayName Get(int id);
        IList<DayName> GetAll();
        bool Update(DayName dayName);
    }
}
