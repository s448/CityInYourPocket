using CityInYourPocket.Interface;
using CityInYourPocket.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CityInYourPocket.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/charity")]
    public class CharityController : Controller
    {
        private readonly ICharity _charity;
        private readonly IHostEnvironment _hostEnvironment;

        public CharityController(ICharity charity, IHostEnvironment hostEnvironment)
        {
            _charity = charity;
            _hostEnvironment = hostEnvironment;
        }

        //get all charities
        // GET: api/charity
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Charity>>> Get()
        {
            return await Task.FromResult(_charity.GetCharities());
        }

        //add charity
        // POST: api/charity
        [HttpPost]
        public async Task<ActionResult<Charity>> Post([FromForm]Charity charity)
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
                charity.Image = path;

                if (ModelState.IsValid)
                {
                    _charity.AddCharity(charity);
                }
                return await Task.FromResult(charity);
            }
            else
            {
                return BadRequest();
            }
        }

        //update charity
        // PUT api/charity/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Charity>> Put([FromForm]int id,[FromForm] Charity charity)
        {
            if (id != charity.Id)
            {
                return BadRequest();
            }
            try
            {
                _charity.UpdateCharity(charity);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_charity.CheckCharity(id))
                {
                    return NotFound("Couldn't find charity");
                }
                else
                {
                    throw;
                }
            }
            return await Task.FromResult(charity);
        }

        //delete charity
        // DELETE api/charity/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<Charity>> Delete(int id)
        {
            var result = _charity.DeleteCharity(id);
            return await Task.FromResult(result);
        }

        //get charity by id
        // GET: api/charity/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Charity>> Get(int id)
        {
            var service = await Task.FromResult(_charity.GetCharity(id));
            return service;
        }
    }
}
