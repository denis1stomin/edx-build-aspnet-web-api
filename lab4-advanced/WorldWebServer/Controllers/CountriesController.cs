using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WorldWebServer.DataAccess;
using WorldWebServer.Models;

namespace WorldWebServer.Controllers
{
    [Route("api/[controller]")]
    public class CountriesController : Controller
    {
        private WorldSqliteDbContext _dbContext;

        public CountriesController()
        {
            var connString = "Data Source=world.db";
            _dbContext = WorldSqliteDbContextFactory.Create(connString);
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok(_dbContext.Country.ToArray());
        }

        [HttpPost]
        public ActionResult Post([FromBody] Country country)
        {
            if (!this.ModelState.IsValid) {
                return BadRequest();
            }

            _dbContext.Country.Add(country);
            _dbContext.SaveChanges();

            return Created($"api/cities/{country.Code}", country);
        }

        [HttpDelete("{code}")]
        public ActionResult Delete(string code)
        {
            Country target = _dbContext.Country.SingleOrDefault(ct => ct.Code == code);
            if (target != null)
            {
                _dbContext.Country.Remove(target);
                _dbContext.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
