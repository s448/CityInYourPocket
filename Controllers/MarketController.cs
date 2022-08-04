using CityInYourPocket.Interface;
using CityInYourPocket.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CityInYourPocket.Controllers
{
    //مكان بيع و شراء كل شيء مثل اوليكس
    [Authorize]
    [ApiController]
    [Route("api/market")]
    public class MarketController : Controller
    {
        private readonly IMarket _market;
        private readonly IHostEnvironment _hostEnvironment;

        public MarketController(IMarket market, IHostEnvironment hostEnvironment)
        {
            _market = market;
            _hostEnvironment = hostEnvironment;
        }

        //search for items in their names and describtions
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Market>>> Serach(string q)
        {
            try
            {
                var result = await _market.Search(q);
                if (result.Any())
                    return Ok(result);
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retreiving data");
            }
        }


        //get all market items
        // GET: api/market
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Market>>> Get()
        {
            return await Task.FromResult(_market.GetMarkets());
        }

        //add market item
        // POST: api/market
        [HttpPost]
        public async Task<ActionResult<Market>> Post([FromForm] Market market)
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
                market.Image = path;

                if (ModelState.IsValid)
                {
                    _market.AddMarket(market);
                }
                return await Task.FromResult(market);
            }
            else
            {
                return BadRequest();
            }
        }

        //update market data
        // PUT api/market/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Market>> Put([FromForm] int id,[FromForm] Market market)
        {
            if (id != market.Id)
            {
                return BadRequest();
            }
            try
            {
                _market.UpdateMarket(market);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_market.CheckMarket(id))
                {
                    return NotFound("Couldn't find news item");
                }
                else
                {
                    throw;
                }
            }
            return await Task.FromResult(market);
        }

        //delete market item
        // DELETE api/market/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<Market>> Delete(int id)
        {
            var result = _market.DeleteMarket(id);
            return await Task.FromResult(result);
        }

        //get market item by id
        // GET: api/market/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Market>> Get(int id)
        {
            var service = await Task.FromResult(_market.GetMarket(id));
            return service;
        }
    }
}
