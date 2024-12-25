using EventManagement_BusinessObjects;
using EventManagement_DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement_Repositories.Interfaces
{
    public interface IEventRepository
    {
        public List<Event> GetAll();
        public Event GetById(string id);
        public void Add(Event eventEntity);
        public void Update(string id, Event eventEntity);
        public void Delete(string id);
    }
}
