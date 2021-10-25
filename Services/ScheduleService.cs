using Microsoft.EntityFrameworkCore;
using PizzaClub.Data;
using PizzaClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaClub.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly ApplicationDbContext _context;

        public ScheduleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Schedule>> AddSchedule(Schedule newSchedule)
        {
            await _context.AddAsync(newSchedule);
            await _context.SaveChangesAsync();
            
            return _context.Schedules.ToList();
        }


        #pragma warning disable CS1998
        public async Task<List<Schedule>> GetAllSchedules()
        #pragma warning restore CS1998
        {
            return _context.Schedules.Include(s => s.Projection).ToList();
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<Schedule> GetScheduleById(int id)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            return _context.Schedules.Include(s => s.Projection).FirstOrDefault(s => s.Id == id);
        }

        public async Task<Schedule> UpdateSchedule(int id, Schedule updateSchedule)
        {
            var schedule = _context.Schedules.Include(s => s.Projection).FirstOrDefault(s => s.Id == id);

            if (schedule != null) 
            {
                schedule.ContractTimeMinutes = updateSchedule.ContractTimeMinutes;
                schedule.Date = updateSchedule.Date;
                schedule.IsFullDayAbsence = updateSchedule.IsFullDayAbsence;
                schedule.Name = updateSchedule.Name;
                schedule.PersonId = updateSchedule.PersonId;
                schedule.Projection = updateSchedule.Projection;
                await _context.SaveChangesAsync();

                return _context.Schedules.FirstOrDefault(s => s.Id == id);
            }

            return null;
        }

        public async Task<Schedule> DeleteSchedule(int id)
        {
            var schedule = _context.Schedules.FirstOrDefault(s => s.Id == id);

            if (schedule != null)
            {
                var deletedSchedule = schedule;
                _context.Remove(schedule);
                await _context.SaveChangesAsync();

                return deletedSchedule;
            }

            return null;
        }

        public async Task<List<Schedule>> DeleteAllSchedules()
        {
            var schedules = _context.Schedules.ToList();
            var deletedSchedules = schedules;
            foreach (var schedule in schedules)
            {
                _context.Remove(schedule);
                await _context.SaveChangesAsync();
            }

            return deletedSchedules;
        }

        public async Task<List<Schedule>> AddOrUpdateSchedule(Schedule newSchedule)
        {
            var oldSchedule = _context.Schedules.FirstOrDefault(s => s.Name == newSchedule.Name && s.Date == newSchedule.Date);
            var newScheduleIsAlreadyPresentInDb = oldSchedule != null;
            if (newScheduleIsAlreadyPresentInDb)
            {
                await UpdateSchedule(oldSchedule.Id, newSchedule);
            }
            else
            {
                await AddSchedule(newSchedule);
            }

            return _context.Schedules.ToList();
        }
    }
}
