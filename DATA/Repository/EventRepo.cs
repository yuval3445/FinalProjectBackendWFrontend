using DATA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA.Repository
{
    public class EventRepo
    {
        private readonly EventsContext _db;

        public EventRepo(EventsContext db)
        {
            _db = db;
        }

        public Event EventByID(int id)
        {
            return _db.Events.FirstOrDefault(p => p.Id == id);
        }

        public List<Event> GetAllEvents()
        {
            return _db.Events.ToList();
        }
        public void InsertEvent(Event e)
        {
            _db.Events.Add(e);
            _db.SaveChanges();
        }
        public List<User> GetUsersByEventId(int eventId)
        {
            return _db.EventUsers
                     .Where(eu => eu.EventRef == eventId)
                     .Select(eu => eu.UserRefNavigation)
                     .ToList();
        }
        public void InsertUserToEvent(int e, int u)
        {
            var registration = new EventUser
            {
                EventRef = e,
                UserRef = u,
                Creation = DateTime.UtcNow
            };

            _db.EventUsers.Add(registration);
            _db.SaveChanges();
        }
        public void UpdateEvent(Event e)
        {
            var EV = _db.Events.FirstOrDefault(p => p.Id == e.Id);

            if (EV == null)
                throw new Exception($"Event with ID {e.Id} not found.");

            // Update the fields you want to allow changing
            EV.Name = e.Name;
            EV.StartDate = e.StartDate;
            EV.EndDate = e.EndDate;
            EV.MaxRegistrations = e.MaxRegistrations;
            EV.Location = e.Location;

            _db.SaveChanges();
        }
        public void DeleteEvent(int eventId)
        {
            var eventToDelete = _db.Events.FirstOrDefault(e => e.Id == eventId);

            if (eventToDelete == null)
                throw new Exception($"Event with ID {eventId} not found.");

            _db.Events.Remove(eventToDelete);
            _db.SaveChanges();
        }

        public List<Event> getEventsInSced(DateTime StartDate, DateTime endtDate)
        {
            return _db.Events.Where(ev => ev.StartDate >= StartDate && ev.EndDate <= endtDate).ToList();
        }

        public string getEventWeather(int eventId)
        {
            String place = _db.Events.Where(ev => ev.Id== eventId).Select(ev => ev.Location).FirstOrDefault();
            if (place == null)
            {
                throw new Exception($"Event with ID {eventId} not found.");
            }
            return place;
        }

    }

}
