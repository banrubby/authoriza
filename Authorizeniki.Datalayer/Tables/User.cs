using System;

namespace Authorizeniki.Datalayer.Tables
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string? Login { get; set; }
        public string? Password { get; set; }

        public string? Surname { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public Guid RoleId { get; set; }

        public virtual Role? UserRole { get; set; }
    }
}