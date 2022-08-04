using CityInYourPocket.Interface;
using CityInYourPocket.Models;
using Microsoft.EntityFrameworkCore;

namespace CityInYourPocket.Repository
{
    public class MarketRepository : IMarket
    {
        private readonly ApplicationDbContext _context;
        public MarketRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public Market AddMarket(Market market)
        {
            try
            {
                _context.markets.Add(market);
                _context.SaveChanges();
            }
            catch
            {
                throw new Exception("Couldn't Save Market data");
            }
            return market;
        }

        public bool CheckMarket(int id)
        {
            try
            {
                Market? market = _context.markets.Find(id);
                if (market != null)
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
                throw new Exception("Failed to return this Market");
            }
        }

        public Market DeleteMarket(int id)
        {
            try
            {
                Market? market = _context.markets.Find(id);
                if (market != null)
                {
                    _context.Remove(market);
                    _context.SaveChanges();
                    return market;
                }
                else
                {
                    throw new ArgumentNullException("Market item Doesn't exist");
                }
            }
            catch
            {
                throw new Exception("Failed to return this Market");
            }
        }

        public Market GetMarket(int id)
        {

            try
            {
                Market? market = _context.markets.Find(id);
                if (market != null)
                {
                    return market;
                }
                else
                {
                    throw new ArgumentNullException("Market Doesn't exist");
                }
            }
            catch
            {
                throw new Exception("Couldn't Return your data for now , please try again later");
            }
        }

        public List<Market> GetMarkets()
        {
            try
            {
                return _context.markets.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Couldn't return Markets List" + ex.ToString());
            }
        }

        public void UpdateMarket(Market market)
        {
            try
            {
                _context.Entry(market).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch
            {
                throw new Exception("Failed to save your changes");
            }
        }

        public async Task<IEnumerable<Market>> Search(string? _query)
        {
            IQueryable<Market> query = _context.markets.AsQueryable();
            query = query.Where(x => x.Name.Contains(_query) || x.Describtion.Contains(_query));

            return await query.ToListAsync();
        }

    }
}
