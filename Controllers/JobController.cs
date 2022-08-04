using CityInYourPocket.Interface;
using CityInYourPocket.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CityInYourPocket.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/job")]
    public class JobController : Controller
    {
        private readonly IJob _job;

        public JobController(IJob job)
        {
            _job = job;
        }

        //search for items in their names and describtions
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Job>>> Serach(string q)
        {
            try
            {
                var result = await _job.Search(q);
                if(result.Any())
                    return Ok(result);
                return NotFound();
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,"Error retreiving data");
            }
        }

        //get all job items
        // GET: api/job
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Job>>> Get()
        {
            return await Task.FromResult(_job.GetJobs());
        }

        //add job item
        // POST: api/market
        [HttpPost]
        public async Task<ActionResult<Job>> Post([FromForm] Job job)
        {
            var result = _job.AddJob(job);
            return await Task.FromResult(result);
        }

        //update job data
        // PUT api/job/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Job>> Put([FromForm] int id, [FromForm] Job job)
        {
            if (id != job.Id)
            {
                return BadRequest();
            }
            try
            {
                _job.UpdateJob(job);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_job.CheckJob(id))
                {
                    return NotFound("Couldn't find Job item");
                }
                else
                {
                    throw;
                }
            }
            return await Task.FromResult(job);
        }

        //delete job item
        // DELETE api/job/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<Job>> Delete(int id)
        {
            var result = _job.DeleteJob(id);
            return await Task.FromResult(result);
        }

        //get job item by id
        // GET: api/job/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Job>> Get(int id)
        {
            var service = await Task.FromResult(_job.GetJob(id));
            return service;
        }
    }
}
