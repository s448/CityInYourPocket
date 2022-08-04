using CityInYourPocket.Models;

namespace CityInYourPocket.Interface
{
    public interface INews
    {
        public List<News> GetNewsList();
        public News GetNewsItem(int id);
        public News DeleteNewsItem(int id);
        public void UpdateNewsItem(News news);
        public News AddNewsItem(News news);
        public bool CheckNewsItem(int id);
    }
}
