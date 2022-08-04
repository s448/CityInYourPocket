using CityInYourPocket.Models;

namespace CityInYourPocket.Interface
{
    public interface ICharity
    {
        public List<Charity> GetCharities();
        public Charity GetCharity(int id);
        public Charity DeleteCharity(int id);
        public void UpdateCharity(Charity charity);
        public Charity AddCharity(Charity charity);
        public bool CheckCharity(int id);
    }
}
