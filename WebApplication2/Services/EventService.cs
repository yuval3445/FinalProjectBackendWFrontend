using DATA.Models;
using DATA.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
namespace WebApplication2.Services
{
    public class EventService
    {
        private readonly EventRepo _EventRepo;

        public EventService(EventRepo eventRepo)
        {
            _EventRepo = eventRepo;
        }
        public Event RetrieveEventByID(int id)
        {
            try
            {
                return _EventRepo.EventByID(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving event by ID", ex);

            }
        }

        public List<Event> RetrieveEvents()
        {
            try
            {
                return _EventRepo.GetAllEvents();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving events", ex);

            }
        }
        public void NewEvent(Event e1)
        {
            try
            {

                _EventRepo.InsertEvent(e1);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving event by ID", ex);
            }
        }

        public List<User> getUsersByEvent(int id)
        {
            try
            {
               return _EventRepo.GetUsersByEventId(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving event by ID", ex);
            }

        }
        public void RegUToEvent(int EventID,int UserID)
        {
            try
            {

                _EventRepo.InsertUserToEvent(EventID,UserID);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving event by ID", ex);
            }
        }
        public void UpEvent(Event e)
        {
            try
            {

                _EventRepo.UpdateEvent(e);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving event by ID", ex);
            }
        }
        public void DelEvent(int eventId)
        {
            try
            {
                _EventRepo.DeleteEvent(eventId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting event", ex);
            }
        }


        public List<Event> getEventsInSce(DateTime StartDate, DateTime endtDate)
        {
            return _EventRepo.getEventsInSced(StartDate, endtDate);
        }

        public string getEventWeather1(int eventId)
        {
            return _EventRepo.getEventWeather(eventId);
        }

    }
}
