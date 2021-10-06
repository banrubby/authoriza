using System;

namespace Authorizeniki.Datalayer.Tables
{
    public class Event
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTime EventTime { get; set; }

        public Guid RfId { get; set; }
        public Guid UserId { get; set; }

        public virtual User? User { get; set; }
    }
}