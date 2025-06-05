using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DATA.Models;
using WebApplication2.Services;
using WebApplication2.DTO;
using System.Net;
using Microsoft.Extensions.Caching.Memory;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly EventService _EventsService;
        private readonly IMemoryCache _memoryCache;

        public EventsController(EventService eventsService, IMemoryCache memoryCache1)
        {
            _EventsService = eventsService;
            _memoryCache=memoryCache1;

        }
        [HttpGet("{EventID}")]
        public ActionResult<Event> GetEventById(int EventID)
        {
            try
            {
                var result = _EventsService.RetrieveEventByID(EventID);
                if (result == null)
                    return NotFound($"Event with ID {EventID} not found");

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while retrieving the event.");
            }
        }
        [HttpPost]
        public ActionResult<string> CreateEvent(Event e)
        {
            try
            {
                _EventsService.NewEvent(e);
                return StatusCode(201, "Event Created");
            }
            catch (Exception ex)
            {
       
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{eventId}/registration")]
        public ActionResult<List<User>> GetUsersEvent(int EventId)
        {
            try
            {
                var result = _EventsService.getUsersByEvent(EventId);
                if (result == null)
                    return NotFound($"Users with EventID {EventId} not found");

                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{eventId}/registration")]
        public ActionResult<string> RegisterUserToEvent(int eventId, RegToEventDTO dto)
        {
            try
            {
                _EventsService.RegUToEvent(eventId, dto.UserId);
                return StatusCode(201, "User successfully registered to the event.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{EventID}")]
        public ActionResult<string> UpdateEvent(int EventID, [FromBody] Event e)
        {
            e.Id = EventID; // Force the body ID to match the URL ID

            try
            {
                _EventsService.UpEvent(e);
                return StatusCode(201, "Event Updated");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{EventID}")]

        public ActionResult DeleteEvent(int EventID)
        {
            try
            {
                _EventsService.DelEvent(EventID);
                return Ok("Event deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("/schedule")]
        public ActionResult<List<Event>> getSchedule(DateTime StartDate, DateTime endDate)
        {
            return Ok(_EventsService.getEventsInSce(StartDate, endDate));
        }

        [HttpGet]
        [Route("{id}/weather")]
        public ActionResult<String> getEventWeather(int id)
        {
            String place = _EventsService.getEventWeather1(id);
            string apiKey = "2ace7b65be1748c0b9a124123250506";
            if (place != null)
            {
                string url = $"http://api.weatherapi.com/v1/forecast.json?key={apiKey}&q={place}";
                string json = (new WebClient()).DownloadString(url);
                _memoryCache.Set("weather", json);
                return Ok(json);
            }
            else
                return Ok("place not found");

        }

    }
}

