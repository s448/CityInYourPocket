using CityInYourPocket.Models;

namespace CityInYourPocket.Interface
{
    public interface IShop
    {
        public List<Shop> GetShops(string category);
        public Shop GetShop(int id);
        public Shop DeleteShop(int id);
        public void UpdateShop(Shop shop);
        public Shop AddShop(Shop shop);
        public bool CheckShop(int id);
        public Task<IEnumerable<Shop>> Search(string _query);
    }
}
