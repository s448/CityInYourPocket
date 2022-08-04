using CityInYourPocket.Interface;
using CityInYourPocket.Models;
using Microsoft.EntityFrameworkCore;

namespace CityInYourPocket.Repository
{
    public class BannerRepository : IBanner
    {
        private readonly ApplicationDbContext _context;
        public BannerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Banner AddBanner(Banner banner)
        {
            try
            {
                _context.banners.Add(banner);
                _context.SaveChanges();
            }
            catch
            {
                throw new Exception("Couldn't Save Banner data");
            }
            return banner;
        }

        public Banner DeleteBanner(int id)
        {
            try
            {
                Banner? banner = _context.banners.Find(id);
                if (banner != null)
                {
                    _context.Remove(banner);
                    _context.SaveChanges();
                    return banner;
                }
                else
                {
                    throw new ArgumentNullException("Banner item Doesn't exist");
                }
            }
            catch
            {
                throw new Exception("Failed to return this Banner");
            }
        }

        public Banner GetBanner(int id)
        {
            try
            {
                Banner? banner = _context.banners.Find(id);
                if (banner != null)
                {
                    return banner;
                }
                else
                {
                    throw new ArgumentNullException("Banner Doesn't exist");
                }
            }
            catch
            {
                throw new Exception("Couldn't Return your data for now , please try again later");
            }
        }

        public void UpdateBanner(Banner banner)
        {
            try
            {
                _context.Entry(banner).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch
            {
                throw new Exception("Failed to save your changes");
            }
        }
    }
}
