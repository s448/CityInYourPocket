using CityInYourPocket.Interface;
using CityInYourPocket.Models;
using Microsoft.EntityFrameworkCore;

namespace CityInYourPocket.Repository
{
    public class CharityRepository : ICharity
    {
        private readonly ApplicationDbContext _context;
        public CharityRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public Charity AddCharity(Charity charity)
        {
            try
            {
                _context.charities.Add(charity);
                _context.SaveChanges();
            }
            catch
            {
                throw new Exception("Couldn't Save Charity data");
            }
            return charity;
        }

        public bool CheckCharity(int id)
        {
            try
            {
                Charity? charity = _context.charities.Find(id);
                if (charity != null)
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
                throw new Exception("Failed to return this Charity");
            }
        }

        public Charity DeleteCharity(int id)
        {
            try
            {
                Charity? charity = _context.charities.Find(id);
                if (charity != null)
                {
                    _context.Remove(charity);
                    _context.SaveChanges();
                    return charity;
                }
                else
                {
                    throw new ArgumentNullException("Job item Doesn't exist");
                }
            }
            catch
            {
                throw new Exception("Failed to return this Charity");
            }
        }

        public Charity GetCharity(int id)
        {

            try
            {
                Charity? charity = _context.charities.Find(id);
                if (charity != null)
                {
                    return charity;
                }
                else
                {
                    throw new ArgumentNullException("Charity Doesn't exist");
                }
            }
            catch
            {
                throw new Exception("Couldn't Return your data for now , please try again later");
            }
        }

        public List<Charity> GetCharities()
        {
            try
            {
                return _context.charities.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Couldn't return Charity List" + ex.ToString());
            }
        }

        public void UpdateCharity(Charity charity)
        {
            try
            {
                _context.Entry(charity).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch
            {
                throw new Exception("Failed to save your changes");
            }
        }
    }
}
