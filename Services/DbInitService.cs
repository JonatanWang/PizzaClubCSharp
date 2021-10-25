using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PizzaClub.Models;
using System;
using System.Collections.Generic;
using System.Net;

namespace PizzaClub.Utils
{
    public class DbInitService :IDbInitService
    {
        public List<Schedule> GetAllSchedulesFromUri(Uri uri)
        {
            var jsonString = new WebClient().DownloadString(uri);
            var scheduleResult = JObject.Parse(jsonString)["ScheduleResult"];
            var jsonResponse = JsonConvert.DeserializeObject<JsonResponse>(scheduleResult.ToString());

            return jsonResponse.Schedules;
        }

        private class JsonResponse
        {
            public List<Schedule> Schedules { get; set; }
        }
    }
}
