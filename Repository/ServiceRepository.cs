using CityInYourPocket.Interface;
using CityInYourPocket.Models;
using Microsoft.EntityFrameworkCore;

namespace CityInYourPocket.Repository
{
    public class ServiceRepository : IService
    {
        private readonly ApplicationDbContext _context;
        public ServiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Service AddService(Service service)
        {
            try
            {
                _context.services.Add(service);
                _context.SaveChanges();
            }
            catch
            {
                throw new Exception("Couldn't Save Service data");
            }
            return service;
        }

        public bool CheckService(int id)
        {
            try
            {
                Service? service = _context.services.Find(id);
                if (service != null)
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
                throw new Exception("Failed to return this service");
            }
        }

        public Service DeleteService(int id)
        {
            try
            {
                Service? service = _context.services.Find(id);
                if (service != null)
                {
                    _context.Remove(service);
                    _context.SaveChanges();
                    return service;
                }
                else
                {
                    throw new ArgumentNullException("Service Doesn't exist");
                }
            }
            catch
            {
                throw new Exception("Failed to return this service");
            }
        }

        public Service GetService(int id)
        {
            try
            {
                Service? service = _context.services.Find(id);
                if (service != null)
                {
                    return service;
                }
                else
                {
                    throw new ArgumentNullException("Service Doesn't exist");
                }
            }
            catch
            {
                throw new Exception("Couldn't Return your data for now , please try again later");
            }
        }

        public List<Service> GetServices()
        {
            try
            {
                return _context.services.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Couldn't return Services List" + ex.ToString());
            }
        }

        public void UpdateService(Service service)
        {
            try
            {
                _context.Entry(service).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch
            {
                throw new Exception("Failed to save your changes");
            }
        }

        public async Task<IEnumerable<Service>> Search(string? _query)
        {
            IQueryable<Service> query = _context.services.AsQueryable();
            query = query.Where(x => x.Name.Contains(_query) || x.Describtion.Contains(_query));

            return await query.ToListAsync();
        }
    }
}
