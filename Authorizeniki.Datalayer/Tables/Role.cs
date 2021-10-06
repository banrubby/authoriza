using System;

namespace Authorizeniki.Datalayer.Tables
{
    public class Role
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string? Name { get; set; }

        public decimal Salary { get; set; }
    }
}