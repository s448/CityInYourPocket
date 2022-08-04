using CityInYourPocket.Models;

namespace CityInYourPocket.Interface
{
    public interface IService
    {
        public List<Service> GetServices(); 
        public Service GetService(int id);
        public Service DeleteService(int id);
        public void UpdateService(Service service);
        public Service AddService(Service service);
        public bool CheckService(int id);
        public Task<IEnumerable<Service>> Search(string _query);
    }
}
