using EventManagement_BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement_DAOs
{
    public class EventDAO : BaseDAO<Event>
    {
        public EventDAO(EventManagementDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Event>> GetEventsAsync(Expression<Func<Event, bool>>? filter = null)
        {
            if (filter != null)
            {
                return await _dbSet.Where(filter)
                    .Include(e => e.Owner)
                    .ToListAsync();
            }

            return await _dbSet.ToListAsync();
        }
    }
}
