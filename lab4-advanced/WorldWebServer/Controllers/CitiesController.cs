using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WorldWebServer.DataAccess;
using WorldWebServer.Models;

namespace WorldWebServer.Controllers
{
    [Route("api/[controller]")]
    public class CitiesController : Controller
    {
        private WorldSqliteDbContext _dbContext;

        public CitiesController()
        {
            var connString = "Data Source=world.db";
            _dbContext = WorldSqliteDbContextFactory.Create(connString);
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok(_dbContext.City.ToArray());
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            City target = _dbContext.City.SingleOrDefault(ct => ct.ID == id);
            if (target != null) {
                return Ok(target);
            } else {
                return NotFound();
            }
        }

        [HttpGet("cc/{cc}")]
        public ActionResult Get(string cc)
        {
            var cities = _dbContext.City
                .Where(ct => string.Equals(ct.CountryCode, cc, StringComparison.CurrentCultureIgnoreCase))
                .ToArray();
            return Ok(cities);
        }

        [HttpPost]
        public ActionResult Post([FromBody] City city)
        {
            if (!this.ModelState.IsValid) {
                return BadRequest();
            }

            _dbContext.City.Add(city);
            _dbContext.SaveChanges();
            return Created($"api/cities/{city.ID}", city);
        }

        [HttpPut("{id}")]
        public ActionResult Put([FromRoute]int id, [FromBody] City city)
        {
            if (!this.ModelState.IsValid) {
                return BadRequest();
            }

            City target = _dbContext.City.SingleOrDefault(ct => ct.ID == id);
            if (target != null) {
                _dbContext.Entry(target).CurrentValues.SetValues(city);
                _dbContext.SaveChanges();
                return Ok();
            } else {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            City target = _dbContext.City.SingleOrDefault(ct => ct.ID == id);
            if (target != null) {
                _dbContext.City.Remove(target);
                _dbContext.SaveChanges();
                return Ok();
            } else {
                return NotFound();
            }
        }
    }
}
