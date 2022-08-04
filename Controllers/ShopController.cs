using CityInYourPocket.Interface;
using CityInYourPocket.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CityInYourPocket.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/shop")]
    public class ShopController : Controller
    {
        private readonly IShop _shops;
        private readonly IHostEnvironment _hostEnvironment;

        public ShopController(IShop shops, IHostEnvironment hostEnvironment)
        {
            _shops = shops;
            _hostEnvironment = hostEnvironment;
        }

        //search for items in their names and describtions
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Shop>>> Serach(string q)
        {
            try
            {
                var result = await _shops.Search(q);
                if (result.Any())
                    return Ok(result);
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retreiving data");
            }
        }


        //get all shops by category 
        // GET: api/shop
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shop>>> Get([FromQuery]string category)
        {
            return await Task.FromResult(_shops.GetShops(category));
        }

        //add single shop
        // POST: api/shop
        [HttpPost]
        public async Task<ActionResult<Shop>> Post([FromForm] Shop shop)
        {
            var files = HttpContext.Request.Form.Files;
            if (files != null && files.Count > 0)
            {
                //get image and save it to the folder with a unigue name
                var imageName = Guid.NewGuid().ToString() + Path.GetExtension(files[0].FileName);
                var path = Path.Combine("", _hostEnvironment.ContentRootPath + "\\Images\\" + imageName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    files[0].CopyTo(stream);
                }
                shop.Image = path;

                if (ModelState.IsValid)
                {
                    _shops.AddShop(shop);
                }
                return await Task.FromResult(shop);
            }
            else
            {
                return BadRequest();
            }
        }

        //update shop data
        // PUT api/shop/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Shop>> Put([FromForm]int id,[FromForm] Shop shop)
        {
            if (id != shop.Id)
            {
                return BadRequest("Id doesn't match");
            }
            try
            {
                _shops.UpdateShop(shop);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_shops.CheckShop(id))
                {
                    return NotFound("Couldn't find shop");
                }
                else
                {
                    throw;
                }
            }
            return await Task.FromResult(shop);
        }

        //delete shop
        // DELETE api/shop/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<Shop>> Delete(int id)
        {
            var result = _shops.DeleteShop(id);
            return await Task.FromResult(result);
        }

        //get shop by id
        // GET: api/shop/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Shop>> Get(int id)
        {
            var service = await Task.FromResult(_shops.GetShop(id));
            return service;
        }
    }
}
