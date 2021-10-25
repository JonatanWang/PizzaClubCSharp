using PizzaClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaClub.Services
{
    public interface IActivityService
    {
        Task<List<Activity>> AddActivity(Activity newActivity);

        Task<Activity> GetActivityById(int id);

        Task<List<Activity>> GetAllActivities();

        Task<Activity> UpdateActivity(int id, Activity updateActivity);

        Task<Activity> DeleteActivity(int id);

        Task<List<Activity>> DeleteAllActivities();
    }
}
