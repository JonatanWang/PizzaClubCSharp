using PizzaClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaClub.Utils
{
    public interface IDbInitTask
    {
        List<Schedule> GetAllSchedulesFromUri(Uri uri);
        Task<List<Schedule>> Execute();
    }
}
