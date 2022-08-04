using CityInYourPocket.Interface;
using CityInYourPocket.Models;
using Microsoft.EntityFrameworkCore;

namespace CityInYourPocket.Repository
{
    public class JobRepository : IJob
    {

        private readonly ApplicationDbContext _context;
        public JobRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public Job AddJob(Job job)
        {
            try
            {
                _context.jobs.Add(job);
                _context.SaveChanges();
            }
            catch
            {
                throw new Exception("Couldn't Save Job data");
            }
            return job;
        }

        public bool CheckJob(int id)
        {
            try
            {
                Job? job = _context.jobs.Find(id);
                if (job != null)
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
                throw new Exception("Failed to return this Job");
            }
        }

        public Job DeleteJob(int id)
        {
            try
            {
                Job? job = _context.jobs.Find(id);
                if (job != null)
                {
                    _context.Remove(job);
                    _context.SaveChanges();
                    return job;
                }
                else
                {
                    throw new ArgumentNullException("Job item Doesn't exist");
                }
            }
            catch
            {
                throw new Exception("Failed to return this Job");
            }
        }

        public Job GetJob(int id)
        {

            try
            {
                Job? job = _context.jobs.Find(id);
                if (job != null)
                {
                    return job;
                }
                else
                {
                    throw new ArgumentNullException("Job Doesn't exist");
                }
            }
            catch
            {
                throw new Exception("Couldn't Return your data for now , please try again later");
            }
        }

        public List<Job> GetJobs()
        {
            try
            {
                return _context.jobs.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Couldn't return Job List" + ex.ToString());
            }
        }

        public void UpdateJob(Job job)
        {
            try
            {
                _context.Entry(job).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch
            {
                throw new Exception("Failed to save your changes");
            }
        }

        public async Task<IEnumerable<Job>> Search(string? _query)
        {
            IQueryable<Job> query = _context.jobs.AsQueryable();
            query = query.Where(x => x.Name.Contains(_query) || x.Describtion.Contains(_query));

            return await query.ToListAsync();
        }
    }
}
