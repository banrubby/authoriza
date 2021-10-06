using System;
using Authorizeniki.Datalayer.Tables;
using Microsoft.EntityFrameworkCore;

namespace Authorizeniki.Datalayer
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DatabaseContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(new Role
            {
                Id = new Guid("B29F65E7-43B9-4A14-9AB2-DDCA2312F2E3"),
                Name = "admin",
                Salary = 999999999
            });

            modelBuilder.Entity<Role>().HasData(new Role
            {
                Id = new Guid("57EB14FB-CFB9-43B6-869D-28BB06E57540"),
                Name = "manager",
                Salary = 1
            });

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = new Guid("869AB7AC-227D-4F45-B6B7-30455BD86A83"),
                Login = "admin",
                Password = "admin",
                Surname = "Админович",
                FirstName = "Админ",
                LastName = "Фсея Руси",
                RoleId = new Guid("B29F65E7-43B9-4A14-9AB2-DDCA2312F2E3")
            });

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = new Guid("1AF8D4A8-0577-4F7B-A917-2083CF3590D7"),
                Login = "manager",
                Password = "manager",
                Surname = "Менеджерович",
                FirstName = "Менеджер",
                LastName = "Фсея менеджеров.рф",
                RoleId = new Guid("57EB14FB-CFB9-43B6-869D-28BB06E57540")
            });

            modelBuilder.Entity<User>(entity => { entity.HasIndex(e => e.Login).IsUnique(); });
        }

        // for migrations
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                $"Server=localhost,1433;Database=Authorizeniki_Db;User ID=SA;Password=Change_this_password;Integrated Security=False;Trusted_Connection=False; MultipleActiveResultSets=true");
        }

        public DbSet<Role> Roles { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Event> Events { get; set; }
    }
}