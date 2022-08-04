using CityInYourPocket.Interface;
using CityInYourPocket.Models;
using Microsoft.EntityFrameworkCore;

namespace CityInYourPocket.Repository
{
    public class UserRepository : IUser
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public User AddUser(User user)
        {
            try
            {
                _context.users.Add(user);
                _context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return user;
        }

        public bool CheckUser(int id)
        {
            try
            {
                User? user = _context.users.Find(id);
                if (user != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                throw new Exception("Failed to return this user");
            }
        }

        public User DeleteUser(int id)
        {
            try
            {
                User? user = _context.users.Find(id);
                if (user != null)
                {
                    _context.Remove(user);
                    _context.SaveChanges();
                    return user;
                }
                else
                {
                    throw new ArgumentNullException("User Doesn't exist");
                }
            }
            catch
            {
                throw new Exception("Failed to return this user");
            }
        }

        public User GetUserById(int id)
        {
            try
            {
                User? user = _context.users.Find(id);
                if (user != null)
                {
                    return user;
                }
                else
                {
                    throw new ArgumentNullException("User Doesn't exist");
                }
            }
            catch
            {
                throw new Exception("Couldn't Return your data for now , please try again later");
            }
        }

        public List<User> GetUsers()
        {
            try
            {
              return  _context.users.ToList();
            }
            catch
            {
                throw new Exception("Couldn't return Users List");
            }
        }

        public void UpdateUser(User user)
        {
            try
            {
                _context.Entry(user).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch
            {
                throw new Exception("Failed to save your changes");
            }
        }
    }
}
