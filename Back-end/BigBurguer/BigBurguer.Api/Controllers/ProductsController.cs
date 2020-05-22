using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BigBurguer.Api.Infrastructure.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Net;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.CodeAnalysis;
using System.Text.Json.Serialization;
using System;
using Microsoft.AspNetCore.Http;

namespace BigBurguer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
             try
            {
                var result = await _context.Products.ToListAsync();
                return Ok(result);
            }
            catch (System.Exception e)
            {
                throw e;

            }
        }

        // GET: api/Products/<id:int>
        // PUT: api/Products/<id:int>
        // POST: api/Products
        [HttpPost]
        public void Post(Product post)
        {
            try
            {
                Product product = new Product
                {
                    ImageUrl = post.ImageUrl,
                    Name = post.Name,
                    Price = post.Price
                };

                _context.Product.Add(product);
                _context.SaveChanges();
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

        // DELETE: api/Products/<id:int>
    }
}
