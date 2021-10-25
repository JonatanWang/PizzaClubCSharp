using Microsoft.AspNetCore.Mvc;
using PizzaClub.Services;
using PizzaClub.Models;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.AspNetCore.Http;

namespace PizzaClub.Controllers
{
    [ApiController]
    [Route("/")]
    public class ActivityController : Controller
    {
        private readonly IActivityService _activityService;
        private readonly ILogger<ActivityController> _logger;

        public ActivityController(IActivityService activityService, ILogger<ActivityController> logger)
        {
            _activityService = activityService;
            _logger = logger;
        }

        [HttpPost("/activity/")]
        public async Task<IActionResult> AddActivity(Activity newActivity)
        {
            try
            {
                return Ok(await _activityService.AddActivity(newActivity));
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to add an activity: {e}.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to add an activity.");
            }
        }

        [HttpGet("/activities")]
        public async Task<IActionResult> GetAllActivities()
        {
            try
            {
                return Ok(await _activityService.GetAllActivities());
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to get all activities: {e}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to get all activities.");
            }
        }

        [HttpGet("/activity/{id}")]
        public async Task<IActionResult> GetActivityById(int id)
        {
            try
            {
                return Ok(await _activityService.GetActivityById(id));
            }
            catch(Exception e)
            {
                _logger.LogError($"Failed to get an activity by id: {id}. Error message: {e}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to get an activity by id: {id}");
            }
        }

        [HttpPut("/activity/{id}")]
        public async Task<IActionResult> UpdateActivity(int id, Activity updateActivity)
        {
            try
            {
                return Ok(await _activityService.UpdateActivity(id, updateActivity));
            }
            catch(Exception e)
            {
                _logger.LogError($"Failed to update an activity by id: {id}. Error message: {e}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to update an activity by id: {id}");
            }
        }

        [HttpDelete("/activity/{id}")]
        public async Task<IActionResult> DeleteActivity(int id)
        {
            try
            {
                return Ok(await _activityService.DeleteActivity(id)); // 204
            } 
            catch(Exception e)
            {
                _logger.LogError($"Failed to delete an activity by id: {id}. Error message: {e}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to delete an activity by id: {id}");
            }
        }

        [HttpDelete("/activities")]
        public async Task<IActionResult> DeleteAllActivities()
        {
            try
            {
                return Ok(await _activityService.DeleteAllActivities());
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to delete all activities: {e}.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to delete all activities.");
            }
        }
    }
}
