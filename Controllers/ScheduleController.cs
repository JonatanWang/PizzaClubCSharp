using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PizzaClub.Models;
using PizzaClub.Services;
using System;
using System.Threading.Tasks;

namespace PizzaClub.Controllers
{
    [ApiController]
    [Route("/")]
    public class ScheduleController : Controller
    {
        private readonly IScheduleService _scheduleService;
        private readonly ILogger<ScheduleController> _logger;

        public ScheduleController(IScheduleService scheduleService, ILogger<ScheduleController> logger)
        {
            _scheduleService = scheduleService;
            _logger = logger;
        }

        [HttpPost("/schedule")]
        public async Task<IActionResult> AddSchedule(Schedule newSchedule)
        {
            try 
            {
                return Ok(await _scheduleService.AddOrUpdateSchedule(newSchedule));
            }
            catch(Exception e)
            {
                _logger.LogError($"Failed to add a new schedule: {e}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Faild to add a new schedule.");
            }
        }

        [HttpGet("/schedules")]
        public async Task<IActionResult> GetAllSchedules()
        {
            try
            {
                return Ok(await _scheduleService.GetAllSchedules());
            } catch(Exception e)
            {
                _logger.LogError($"Failed to get schedules: {e}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Faild to get schedules.");
            }     
        }

        [HttpGet("/schedule/{id:int}")]
        public async Task<IActionResult> GetScheduleById(int id)
        {
            try
            {
                var schedule = await _scheduleService.GetScheduleById(id);
                if (schedule == null) 
                {
                    return StatusCode(StatusCodes.Status404NotFound, $"Failed to find the schedule by id: {id}");
                }
                return Ok(schedule);

            } catch(Exception e)
            {
                _logger.LogError($"Failed to get schedule by id({id}): {e}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Faild to get schedule.");
            }
        }

        [HttpPut("/schedule/{id:int}")]
        public async Task<IActionResult> UpdateSchedule(int id, Schedule updateSchedule)
        {
            try 
            {
                return Ok(await _scheduleService.UpdateSchedule(id, updateSchedule));
            }
            catch(Exception e)
            {
                _logger.LogError($"Failed to update schedule by id({id}): {e}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Faild to update schedule.");
            }     
        }

        [HttpDelete("/schedule/{id:int}")]
        public async Task<IActionResult> DeleteSchedule(int id)
        {
            try 
            {
                return Ok(await _scheduleService.DeleteSchedule(id));
            }
            catch(Exception e)
            {
                _logger.LogError($"Failed to delete schedule by id({id}): {e}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Faild to delete schedule.");
            }
        }

        [HttpDelete("/schedules")]
        public async Task<IActionResult> DeleteAllSchedules()
        {
            try
            {
                return Ok(await _scheduleService.DeleteAllSchedules());
            }
            catch(Exception e)
            {
                _logger.LogError($"Failed to delete all schedules: {e}.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to delete all schedules.");
            }
        }
    }
}
