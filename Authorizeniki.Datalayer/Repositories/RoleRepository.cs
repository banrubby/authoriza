using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authorizeniki.Datalayer.Tables;

namespace Authorizeniki.Datalayer.Repositories
{
    public interface IRoleRepository
    {
        void Add(Role role);
        List<Role> GetRoles();
        Guid? GetRoleIdByName(string name);
    }

    public class RoleRepository : IRoleRepository
    {
        private readonly DatabaseContext context;

        public RoleRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public List<Role> GetRoles()
        {
            return context.Roles.ToList();
        }

        public Guid? GetRoleIdByName(string name)
        {
            return context.Roles.FirstOrDefault(item => item.Name == name)?.Id;
        }

        public void Add(Role role)
        {
            context.Roles.Add(role);
            context.SaveChanges();
        }
    }
}