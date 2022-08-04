using CityInYourPocket.Models;

namespace CityInYourPocket.Interface
{
    public interface IMarket
    {
        public List<Market> GetMarkets();
        public Market GetMarket(int id);
        public Market DeleteMarket(int id);
        public void UpdateMarket(Market market);
        public Market AddMarket(Market market);
        public bool CheckMarket(int id);
        public Task<IEnumerable<Market>> Search(string _query);
    }
}
