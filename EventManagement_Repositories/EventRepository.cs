using EventManagement_BusinessObjects;
using EventManagement_DAOs;
using EventManagement_Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement_Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly EventDAO _eventDAO;

        public EventRepository(EventDAO eventDAO)
        {
            _eventDAO = eventDAO;
        }
        public void Add(Event eventEntity) => _eventDAO.Add(eventEntity);

        public void Delete(string id) => _eventDAO.Delete(id);

        public List<Event> GetAll() => _eventDAO.GetAll();

        public Event GetById(string id) => _eventDAO.GetById(id);

        public void Update(string id, Event eventEntity) => _eventDAO.Update(id, eventEntity);
    }
}
