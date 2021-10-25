using PizzaClub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaClub.Services
{
    public interface IScheduleService
    {
        Task<List<Schedule>> AddSchedule(Schedule newSchedule);

        Task<List<Schedule>> AddOrUpdateSchedule(Schedule newSchedule);

        Task<Schedule> GetScheduleById(int id);

        Task<List<Schedule>> GetAllSchedules();

        Task<Schedule> UpdateSchedule(int id, Schedule updateSchedule);

        Task<Schedule> DeleteSchedule(int id);

        Task<List<Schedule>> DeleteAllSchedules();
    }
}
