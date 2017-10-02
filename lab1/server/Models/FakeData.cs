using System.Collections.Generic;

namespace server.Models
{
    public class FakeData
    {
        public static IList<Product> Items { get; set; }

        static FakeData() {
            Items = new List<Product>();
            Items.Add(new Product { ID = 0, Name = "Apple", Price = 7.8 });
            Items.Add(new Product { ID = 1, Name = "Orange", Price = 10.9 });
            Items.Add(new Product { ID = 2, Name = "Banana", Price = 42.1 });
            Items.Add(new Product { ID = 3, Name = "Grape", Price = 3.0 });
        }
    }
}
