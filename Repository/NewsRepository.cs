using CityInYourPocket.Interface;
using CityInYourPocket.Models;
using Microsoft.EntityFrameworkCore;

namespace CityInYourPocket.Repository
{
    public class NewsRepository : INews
    {
        private readonly ApplicationDbContext _context;
        public NewsRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public News AddNewsItem(News news)
        {
            try
            {
                _context.news.Add(news);
                _context.SaveChanges();
            }
            catch
            {
                throw new Exception("Couldn't Save News data");
            }
            return news;
        }

        public bool CheckNewsItem(int id)
        {
            try
            {
                News? news = _context.news.Find(id);
                if (news != null)
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
                throw new Exception("Failed to return this news item");
            }
        }

        public News DeleteNewsItem(int id)
        {
            try
            {
                News? news = _context.news.Find(id);
                if (news != null)
                {
                    _context.Remove(news);
                    _context.SaveChanges();
                    return news;
                }
                else
                {
                    throw new ArgumentNullException("News item Doesn't exist");
                }
            }
            catch
            {
                throw new Exception("Failed to return this news item");
            }
        }

        public News GetNewsItem(int id)
        {

            try
            {
                News? news = _context.news.Find(id);
                if (news != null)
                {
                    return news;
                }
                else
                {
                    throw new ArgumentNullException("News Doesn't exist");
                }
            }
            catch
            {
                throw new Exception("Couldn't Return your data for now , please try again later");
            }
        }

        public List<News> GetNewsList()
        {
            try
            {
                return _context.news.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Couldn't return Services List" + ex.ToString());
            }
        }

        public void UpdateNewsItem(News news)
        {
            try
            {
                _context.Entry(news).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch
            {
                throw new Exception("Failed to save your changes");
            }
        }
    }
}
