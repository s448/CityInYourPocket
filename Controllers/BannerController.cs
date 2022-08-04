using CityInYourPocket.Interface;
using CityInYourPocket.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CityInYourPocket.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/banner")]
    public class BannerController : Controller
    {
        private readonly IBanner _banner;
        private readonly IHostEnvironment _hostEnvironment;

        public BannerController(IBanner banner, IHostEnvironment hostEnvironment)
        {
            _banner = banner;
            _hostEnvironment = hostEnvironment;
        }

        //add banner
        // POST: api/banner
        [HttpPost]
        public async Task<ActionResult<Banner>> Post([FromForm] Banner banner)
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
                banner.BannerImage = path;

                if (ModelState.IsValid)
                {
                    _banner.AddBanner(banner);
                }
                return await Task.FromResult(banner);
            }
            else
            {
                return BadRequest();
            }
        }

        //delete banner
        // DELETE api/banner/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<Banner>> Delete(int id)
        {
            var result = _banner.DeleteBanner(id);
            return await Task.FromResult(result);
        }

        //get banner by id
        // GET: api/banner/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Banner>> Get(int id)
        {
            var service = await Task.FromResult(_banner.GetBanner(id));
            return service;
        }
    }
}
