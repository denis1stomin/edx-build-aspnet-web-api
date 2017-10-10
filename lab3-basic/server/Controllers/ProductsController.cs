using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using server.Models;

namespace server.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        public class PriceFilter
        {
            public double? MinPrice {get; set;}
            public double? MaxPrice {get; set;}
        }

        [HttpGet]
        public ActionResult Get([FromQuery]PriceFilter filter)
        {
            if (!filter.MinPrice.HasValue && !filter.MaxPrice.HasValue)
                return Ok(FakeData.Products.Values);
            
            if (!filter.MinPrice.HasValue)
                filter.MinPrice = int.MinValue;
            if (!filter.MaxPrice.HasValue)
                filter.MaxPrice = int.MaxValue;

            var items = FakeData.Products
                .Where(p => filter.MinPrice <= p.Value.Price && p.Value.Price <= filter.MaxPrice)
                .Select(p => p.Value);

            return Ok(items);
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var item = FakeData.Products.SingleOrDefault(p => p.Key == id).Value;
            if (item != null)
                return Ok(item);
            else
                return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (FakeData.Products.ContainsKey(id))
            {
                FakeData.Products.Remove(id);
                return Ok();
            }
            else
                return NotFound();
        }

        [HttpPost]
        public ActionResult Post([FromBody]Product product)
        {
            product.ID = (IdCnt++);
            FakeData.Products.Add(product.ID, product);

            return Ok(product);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody]Product product)
        {
            if (id != product.ID)
                return BadRequest();

            var item = FakeData.Products.SingleOrDefault(p => p.Key == id).Value;
            if (item != null)
            {
                item.Name = product.Name;
                item.Price = product.Price;

                return Ok();
            }
            
            return NotFound();
        }

        [HttpPut("[action]/{value}")]
        public ActionResult Raise(double value)
        {
            foreach (var item in FakeData.Products)
                item.Value.Price += value;

            return Ok(FakeData.Products.Values);
        }

        private static int IdCnt = FakeData.Products.Keys.Max();
    }
}
