using System;
using System.Collections.Generic;
using System.Linq;
using Authorizeniki.Datalayer.Tables;
using Microsoft.EntityFrameworkCore;

namespace Authorizeniki.Datalayer.Repositories
{
    public interface IUserRepository
    {
        User? GetUserById(Guid id);
        User? GetUserByLogin(string login);
        List<User> GetUsers(int skip, int take);
        User Add(User userToAdd);
    }

    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext context;

        public UserRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public User? GetUserById(Guid id)
        {
            return context
                .Users
                .Include(user => user.UserRole)
                .FirstOrDefault(user => user.Id == id);
        }

        public List<User> GetUsers(int skip, int take)
        {
            return context.Users
                .Include(user => user.UserRole)
                .Skip(skip)
                .Take(take)
                .ToList();
        }

        public User? GetUserByLogin(string login)
        {
            return context.Users
                .Include(user => user.UserRole)
                .FirstOrDefault(user => user.Login == login);
        }

        public User Add(User userToAdd)
        {
            context.Users.Add(userToAdd);
            context.SaveChanges();
            return userToAdd;
        }
    }
}