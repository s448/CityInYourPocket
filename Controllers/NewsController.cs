using CityInYourPocket.Interface;
using CityInYourPocket.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CityInYourPocket.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/news")]
    public class NewsController : Controller
    {
        private readonly INews _news;
        private readonly IHostEnvironment _hostEnvironment;

        public NewsController(INews news, IHostEnvironment hostEnvironment)
        {
            _news = news;
            _hostEnvironment = hostEnvironment;
        }

        //get all news
        // GET: api/news
        [HttpGet]
        public async Task<ActionResult<IEnumerable<News>>> Get()
        {
            return await Task.FromResult(_news.GetNewsList());
        }

        //add news item
        // POST: api/news
        [HttpPost]
        public async Task<ActionResult<News>> Post([FromForm] News news)
        {
            var files = HttpContext.Request.Form.Files;
            if (files != null && files.Count > 0)
            {
                var imageName = Guid.NewGuid().ToString() + Path.GetExtension(files[0].FileName);
                var path = Path.Combine("", _hostEnvironment.ContentRootPath + "\\Images\\" + imageName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    files[0].CopyTo(stream);
                }
                news.Image = path;

                if (ModelState.IsValid)
                {
                    _news.AddNewsItem(news);
                }
                return await Task.FromResult(news);
            }
            else
            {
                return BadRequest();
            }
        }

        //update news data
        // PUT api/news/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<News>> Put(int id, News news)
        {
            if (id != news.Id)
            {
                return BadRequest();
            }
            try
            {
                _news.UpdateNewsItem(news);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_news.CheckNewsItem(id))
                {
                    return NotFound("Couldn't find news item");
                }
                else
                {
                    throw;
                }
            }
            return await Task.FromResult(news);
        }

        //delete news item
        // DELETE api/news/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<News>> Delete(int id)
        {
            var result = _news.DeleteNewsItem(id);
            return await Task.FromResult(result);
        }

        //get news item by id
        // GET: api/news/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<News>> Get(int id)
        {
            var service = await Task.FromResult(_news.GetNewsItem(id));
            return service;
        }
    }
}
