using Microsoft.EntityFrameworkCore;
using Users_Api.Models;

namespace Users_Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbContextFactory<UserDBContext> _userContext;

        public UserRepository(IDbContextFactory<UserDBContext> userContext)
        {
            _userContext = userContext;
        }

        public void AddUser(User tarea)
        {
            using var context = _userContext.CreateDbContext();
            context.Users.Add(tarea);
            context.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            using var context = _userContext.CreateDbContext();
            var user = context.Users.Find(id);
            if (user != null)
            {
                context.Users.Remove(user);
                context.SaveChanges();
            }
        }

        public IEnumerable<User> GetAllUsers()
        {
            using var context = _userContext.CreateDbContext();
            return context.Users.ToList();
        }

        public User GetUserById(int id)
        {
            using var context = _userContext.CreateDbContext();
            return context.Users.Find(id);
        }

        public void UpdateUser(User user)
        {
            using var context = _userContext.CreateDbContext();
            context.Users.Update(user);
            context.SaveChanges();
        }
    }
}
