using Microsoft.EntityFrameworkCore;
using StockExchange.Server.Context;
using StockExchange.Server.Model;

namespace StockExchange.Server.Repository
{
    public interface IUserRepository
    {
        void AddUser(User User);
        User GetUser(int id);
        IEnumerable<User> GetUsers();
        void UpdateUser(User User);
        void DeleteUser(int id);
    }

    public class UserRepository : IUserRepository
    {
        private readonly StockExchangeApiContext _context;

        public UserRepository(StockExchangeApiContext context)
        {
            _context = context;
        }

        public void AddUser(User User)
        {
            _context.Users.Add(User);
            _context.SaveChanges();
        }

        public User GetUser(int id)
        {
            return _context.Users.Find(id);
        }

        public IEnumerable<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public void UpdateUser(User User)
        {
            _context.Entry(User).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            var User = _context.Users.Find(id);
            _context.Users.Remove(User);
            _context.SaveChanges();
        }
    }
}