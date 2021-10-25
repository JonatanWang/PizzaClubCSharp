using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PizzaClub.Models;
using PizzaClub.Services;
using PizzaClub.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaClub.Controllers
{
    [ApiController]
    [Route("/")]
    public class DbInitController : Controller
    {
        private readonly IDbInitService _dbInitService;
        private readonly IScheduleService _scheduleService;
        private readonly ILogger<DbInitController> _logger;
        private readonly Uri uri = new Uri("http://pizzacabininc.azurewebsites.net/PizzaCabinInc.svc/schedule/2015-12-14");
        public DbInitController(IDbInitService dbInitService, IScheduleService scheduleService, ILogger<DbInitController> logger)
        {
            _dbInitService = dbInitService;
            _scheduleService = scheduleService;
            _logger = logger;
        }

        [HttpGet("/initDb")]
        public async Task<IActionResult> AddScheduleToDb()
        {
            try
            {
                var _schedules = _dbInitService.GetAllSchedulesFromUri(uri);
                for (var i = 0; i < _schedules.Count; i++)
                {
                    await _scheduleService.AddOrUpdateSchedule(_schedules[i]);
                }

                return Ok(await _scheduleService.GetAllSchedules());
            }
            catch(Exception e)
            {
                _logger.LogError($"Server failed: {e}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Server failed.");
            }
        }

        [HttpPost("/initDb")]
        public async Task<IActionResult> AddSchedulesFromJson(JsonBody jsonBody)
        {
            try
            {
                var _schedules = jsonBody.ScheduleResult.Schedules;
                for (var i = 0; i < _schedules.Count; i ++)
                {
                    await _scheduleService.AddOrUpdateSchedule(_schedules[i]);
                }

                return Ok(await _scheduleService.GetAllSchedules());
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to initialize database: {e}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to initialize database.");
            }
        }

        public class JsonBody
        {
            public JsonBodySchedules ScheduleResult { get; set; }
        }
        public class JsonBodySchedules
        {
            public List<Schedule> Schedules { get; set; }
        }
    }
}
