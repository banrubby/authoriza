using System;
using System.Collections.Generic;
using System.Linq;
using Authorizeniki.Datalayer.Tables;
using Microsoft.EntityFrameworkCore;

namespace Authorizeniki.Datalayer.Repositories
{
    public interface IEventRepository
    {
        void Add(Event evnt);
        List<Event> GetEventsByUserId(Guid userId, DateTime from, DateTime to);
    }

    public class EventRepository : IEventRepository
    {
        private readonly DatabaseContext context;

        public EventRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public void Add(Event evnt)
        {
            context.Events.Add(new Event()
            {
                EventTime = DateTime.Now,
                RfId = evnt.RfId,
                UserId = evnt.UserId
            });
            context.SaveChanges();
        }

        public List<Event> GetEventsByUserId(Guid userId, DateTime from, DateTime to)
        {
            return context
                .Events.Include(e => e.User).ThenInclude(u => u.UserRole)
                .Where(e => e.UserId == userId && e.EventTime > from && e.EventTime < to)
                .ToList();
        }
    }
}