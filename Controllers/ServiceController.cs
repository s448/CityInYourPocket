using CityInYourPocket.Interface;
using CityInYourPocket.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CityInYourPocket.Controllers
{
    [Authorize]
    [Route("api/service")]
    [ApiController]
    public class ServiceController : Controller
    {
        private readonly IService _services;
        private readonly IHostEnvironment _hostEnvironment;
        public ServiceController (IService services, IHostEnvironment hostEnvironment)
        {
            _services = services;
            _hostEnvironment = hostEnvironment;
        }

        //search for items in their names and describtions
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Service>>> Serach(string q)
        {
            try
            {
                var result = await _services.Search(q);
                if (result.Any())
                    return Ok(result);
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retreiving data");
            }
        }


        //get all services
        // GET: api/service
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Service>>> Get()
        {
            return await Task.FromResult(_services.GetServices());
        }

        //add service
        // POST: api/service
        [HttpPost]
        public async Task<ActionResult<Service>> Post([FromForm]Service service)
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
                service.Image = path;

                if (ModelState.IsValid)
                {
                    _services.AddService(service);
                }
                return await Task.FromResult(service);
            }
            else
            {
                return BadRequest();
            }
        }

        //update service data
        // PUT api/service/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Service>> Put(int id, Service service)
        {
            if (id != service.Id)
            {
                return BadRequest();
            }
            try
            {
                _services.UpdateService(service);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_services.CheckService(id))
                {
                    return NotFound("Couldn't find service");
                }
                else
                {
                    throw;
                }
            }
            return await Task.FromResult(service);
        }

        //delete service
        // DELETE api/service/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<Service>> Delete(int id)
        {
            var result = _services.DeleteService(id);
            return await Task.FromResult(result);
        }

        //get service by id
        // GET: api/service/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Service>> Get(int id)
        {
            var service = await Task.FromResult(_services.GetService(id));
            return service;
        }

    }
}
