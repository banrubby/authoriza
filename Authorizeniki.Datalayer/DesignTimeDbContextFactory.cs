using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Authorizeniki.Datalayer
{
    public class DesignTimeDbContextFactory: IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<DatabaseContext>();
            builder.UseSqlServer($"Server=localhost,1433;Database=Authorizeniki_Db;User ID=SA;Password=change_this_password;Integrated Security=False");
            return new DatabaseContext(builder.Options);
        }
    }
}