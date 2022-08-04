using CityInYourPocket.Interface;
using CityInYourPocket.Models;
using Microsoft.EntityFrameworkCore;

namespace CityInYourPocket.Repository
{
    public class ShopRepository : IShop
    {
        private readonly ApplicationDbContext _context;
        public ShopRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Shop AddShop(Shop shop)
        {
            try
            {
                _context.shops.Add(shop);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Couldn't add New Shop Info err= " + ex);
            }
            return shop;
        }

        public bool CheckShop(int id)
        {
            try
            {
                Shop? shop = _context.shops.Find(id);
                if (shop != null)
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
                throw new Exception("Failed to return this Shop");
            }
        }

        public Shop DeleteShop(int id)
        {
            try
            {
                Shop? shop = _context.shops.Find(id);
                if (shop != null)
                {
                    _context.Remove(shop);
                    _context.SaveChanges();
                    return shop;
                }
                else
                {
                    throw new ArgumentNullException("Shop Doesn't exist");
                }
            }
            catch
            {
                throw new Exception("Failed to return this Shop");
            }
        }

        public Shop GetShop(int id)
        {
            try
            {
                Shop? shop = _context.shops.Find(id);
                if (shop != null)
                {
                    return shop;
                }
                else
                {
                    throw new ArgumentNullException("Shop Doesn't exist");
                }
            }
            catch
            {
                throw new Exception("Couldn't Return your data for now , please try again later");
            }
        }

        public List<Shop> GetShops(string category)
        {
            try
            {
                var result = (from shop in _context.shops where shop.Category == category select shop).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Couldn't return Shops List" + ex.ToString());
            }
        }

        public void UpdateShop(Shop shop)
        {
            try
            {
                _context.Entry(shop).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch
            {
                throw new Exception("Failed to save your changes");
            }
        }

        public async Task<IEnumerable<Shop>> Search(string? _query)
        {
            IQueryable<Shop> query = _context.shops.AsQueryable();
            query = query.Where(x => x.Name.Contains(_query) || x.Describtion.Contains(_query));

            return await query.ToListAsync();
        }
    }
}
