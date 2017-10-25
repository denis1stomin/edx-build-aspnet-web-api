using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WorldWebServer.DataAccess;

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
    }
}
