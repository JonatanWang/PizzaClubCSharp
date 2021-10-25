using Microsoft.EntityFrameworkCore;
using PizzaClub.Data;
using PizzaClub.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaClub.Services
{
    public class ActivityService : IActivityService
    {
        private readonly ApplicationDbContext _context;

        public ActivityService(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task<List<Activity>> AddActivity(Activity newActivity)
        {
            await _context.AddAsync(newActivity);
            await _context.SaveChangesAsync();

            return _context.Activities.ToList();
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<Activity> GetActivityById(int id)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            return _context.Activities.Include(a => a.Schedule).FirstOrDefault(a => a.Id == id);
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<List<Activity>> GetAllActivities()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            return _context.Activities.Include(a => a.Schedule).ToList();
        }

        public async Task<Activity> UpdateActivity(int id, Activity updateActivity)
        {
            var activity = _context.Activities.FirstOrDefault(a => a.Id == id);
            if (activity != null) 
            {
                activity.Color = updateActivity.Color;
                activity.Description = updateActivity.Description;
                activity.Start = updateActivity.Start;
                activity.minutes = updateActivity.minutes;
                await _context.SaveChangesAsync();

                return _context.Activities.FirstOrDefault(a => a.Id == id);
            }

            return null;
        }

        public async Task<Activity> DeleteActivity(int id)
        {
            var activity = _context.Activities.FirstOrDefault(a => a.Id == id);
            if (activity != null) 
            {
                var deletedActivity = activity;
                _context.Remove(activity);
                await _context.SaveChangesAsync();

                return deletedActivity;
            }
            
            return null;
        }

        public async Task<List<Activity>> DeleteAllActivities()
        {
            var activities = _context.Activities.Include(a => a.Schedule).ToList();
            var deletedActivities = activities;
            foreach(var activity in activities)
            {
                _context.Remove(activity);
                await _context.SaveChangesAsync();
            }
            return deletedActivities;
        }
    }
}
