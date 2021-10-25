using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PizzaClub.Models;
using PizzaClub.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PizzaClub.Utils
{
    public class DbInitTask : IDbInitTask
    {
        private readonly Uri uri = new Uri("http://pizzacabininc.azurewebsites.net/PizzaCabinInc.svc/schedule/2015-12-14");
        private readonly IScheduleService _scheduleService;

        public DbInitTask(IScheduleService scheduleService) 
        {
            _scheduleService = scheduleService;
        }
   

        public async Task<List<Schedule>> Execute()
        {
            var schedules = GetAllSchedulesFromUri(uri);
            foreach (var schedule in schedules)
            {
                await _scheduleService.AddSchedule(schedule);
            }
            return schedules;
        }

        public List<Schedule> GetAllSchedulesFromUri(Uri uri)
        {
            var jsonString = new WebClient().DownloadString(uri);
            var scheduleResult = JObject.Parse(jsonString)["ScheduleResult"];
            var jsonResponse = JsonConvert.DeserializeObject<JsonResponse>(scheduleResult.ToString());
            var schedules = jsonResponse.Schedules;
            Console.WriteLine("Schedules: " + schedules.ToString());

            return schedules;
        }

        private class JsonResponse
        {
            public List<Schedule> Schedules { get; set; }
        }
    }
}
