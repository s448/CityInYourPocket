using CityInYourPocket.Models;

namespace CityInYourPocket.Interface
{
    public interface IBanner
    {
        public Banner GetBanner(int id);
        public Banner DeleteBanner(int id);
        public void UpdateBanner(Banner banner);
        public Banner AddBanner(Banner banner);
    }
}
