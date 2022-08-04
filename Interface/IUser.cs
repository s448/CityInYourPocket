using CityInYourPocket.Models;

namespace CityInYourPocket.Interface
{
    public interface IUser
    {
        public List<User> GetUsers();
        public User GetUserById(int id);
        public User DeleteUser(int id);
        public bool CheckUser(int id);
        public void UpdateUser(User user);
        public User AddUser(User user);
    }
}
