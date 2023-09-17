using kaiDeeMak.Data;
using kaiDeeMak.Models;
using Microsoft.AspNetCore.Mvc;

namespace kaiDeeMak.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : Controller
    {

        private readonly ApplicationDBContext _db;
        public ProductController(ApplicationDBContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        // /Product/AllProduct
        [HttpGet]
        [Route("AllProduct")]
        public IActionResult AllProduct()
        {
            IEnumerable<Products> allProduct = _db.Products;

            return Ok(allProduct);
        }

        // /Product/GetProductByID/{id}
        [HttpGet]
        [Route("GetProductByID/{id}")]
        [IgnoreAntiforgeryToken]
        public IActionResult GetProductByID(int id)
        {
            var product = _db.Products.Find(id);
            if (product == null || id == 0)
            {
                return NotFound();
            }
            else if (ModelState.IsValid)
            {
                return Ok(product);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // /Product/CreateProduct
        [HttpPost]
        [Route("CreateProduct")]
        [IgnoreAntiforgeryToken]
        public IActionResult CreateProduct([FromForm] Products model)
        {
            if (ModelState.IsValid)
            {
                _db.Products.Add(model);
                _db.SaveChanges();
                return Ok("สร้างเรียบร้อย");
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // /Product/ShowCreateProduct
        [HttpGet]
        [Route("ShowCreateProduct")]
        public IActionResult ShowCreateProduct()
        {
            return View();
        }

        // /Product/UpdateProduct/{id}
        [HttpPost]
        [Route("UpdateProduct/{id}")]
        [IgnoreAntiforgeryToken]
        public IActionResult UpdateProduct([FromForm] Products model)
        {
                _db.Products.Update(model);
                _db.SaveChanges();
                return Ok("แก้ไขเรียบร้อย");
        }

        // /Product/ShowUpdateProduct/{id}
        [HttpGet]
        [Route("ShowUpdateProduct/{id}")]
        public IActionResult ShowUpdateProduct(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Products.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        // /Product/DeleteProduct/{id}
        [HttpDelete()]
        [Route("DeleteProduct/{id}")]
        [IgnoreAntiforgeryToken]
        public IActionResult DeleteProduct(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Products.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Products.Remove(obj);
            _db.SaveChanges();
            return Ok("ลบเรียบร้อย");
        }

    }
}
