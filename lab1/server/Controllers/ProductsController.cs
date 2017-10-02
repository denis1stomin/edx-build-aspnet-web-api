using System.Linq;
using Microsoft.AspNetCore.Mvc;
using server.Models;

namespace server.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        [HttpGet]
        public Product[] Get() {
            return FakeData.Items.ToArray();
        }

        [HttpGet("{id}")]
        public ActionResult Get(int? id) {
            if (id == null) return null;
            var rocket = FakeData.Items.SingleOrDefault(r => r.ID == id.Value);
            return Ok(rocket);
        }
    }
}
