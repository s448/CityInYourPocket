using CityInYourPocket.Models;

namespace CityInYourPocket.Interface
{
    public interface IJob
    {
        public List<Job> GetJobs();
        public Job GetJob(int id);
        public Job DeleteJob(int id);
        public void UpdateJob(Job job);
        public Job AddJob(Job job);
        public bool CheckJob(int id);
        public Task<IEnumerable<Job>> Search(string _query);
    }
}
