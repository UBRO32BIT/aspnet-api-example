using EventManagement_BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement_Repositories
{
    public class EventInvitationRepository : BaseRepository<EventInvitation>
    {
        public EventInvitationRepository(IUnitOfWork<EventManagementDbContext> unitOfWork) : base(unitOfWork) { }

        public EventInvitationRepository(EventManagementDbContext dbContext) : base(dbContext) { }

        public async Task<IEnumerable<EventInvitation>> GetInvitationsAsync(Expression<Func<EventInvitation, bool>>? filter = null)
        {
            if (filter != null)
            {
                return await _dbSet.Where(filter).Include(e => e.Invitor).Include(e => e.Event).ToListAsync();
            }
            return await _dbSet.Include(e => e.Invitor).Include(e => e.Event).ToListAsync();
        }
    }
}
