using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi1.Models;

namespace WebApi1.Controllers
{
    public class ProductsController : ApiController
    {
        static List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 },
            new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M },
            new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M }
        };

        public IEnumerable<Product> GetAllProducts()
        {
            return products;
        }

        public IHttpActionResult GetProduct(int id)
        {
            var product = products.FirstOrDefault((p) => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        public IHttpActionResult PostProduct(Product product)
        {
            products.Add(product);
            //用Created的狀態碼是201
            return Created(Url.Route("DefaultApi", new { id = product.Id }), products);
        }

        public IHttpActionResult DeleteProduct(int id)
        {
            var product = products.Where(p => p.Id == id).FirstOrDefault();
            products.Remove(product);
            //用Created的狀態碼是201
            return Ok(products);
        }
    }
}
