using EventManagement_BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement_DAOs
{
    public class EventDAO
    {
        private readonly EventManagementDbContext _context;

        public EventDAO(EventManagementDbContext context)
        {
            _context = context;
        }

        public List<Event> GetAll()
        {
            try
            {
                return _context.Events.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public Event GetById(string id)
        {
            try
            {
                return _context.Events.Where(e => e.Id.ToString().Equals(id)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public void Add(Event eventEntity)
        {
            try
            {
                _context.Add(eventEntity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        public void Update(string id, Event eventEntity)
        {
            try
            {
                var existedEvent = _context.Events.Where(_e => _e.Id.ToString().Equals(id)).FirstOrDefault();
                if (existedEvent == null)
                {
                    throw new Exception("Event not found");
                }
                existedEvent.Name = eventEntity.Name;
                existedEvent.Description = eventEntity.Description;
                _context.Update(existedEvent);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        public void Delete(string id)
        {
            try
            {
                var existedEvent = _context.Events.Where(_e => _e.Id.ToString().Equals(id)).FirstOrDefault();
                if (existedEvent == null)
                {
                    throw new Exception("Event not found");
                }
                existedEvent.IsDeleted = true;
                _context.Update(existedEvent);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
