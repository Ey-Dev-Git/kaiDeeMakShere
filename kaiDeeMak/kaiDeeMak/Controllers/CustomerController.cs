using kaiDeeMak.Data;
using kaiDeeMak.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace kaiDeeMak.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {

        private readonly ApplicationDBContext _db;
        public CustomerController(ApplicationDBContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        // /Customer/AllCustomer
        [HttpGet]
        [Route("AllCustomer")]
        public IActionResult AllCustomer()
        {
            IEnumerable<Customers> allCustomer = _db.Customers;

            return Ok(allCustomer);
        }


        // /Customer/GetCustomerByID/{id}
        [HttpGet]
        [Route("GetCustomerByID/{id}")]
        [IgnoreAntiforgeryToken]
        public IActionResult GetCustomerByID(int id)
        {
            var customer = _db.Customers.Find(id);
            if (customer == null || id == 0)
            {
                return NotFound();
            }
            else if (ModelState.IsValid)
            {
                return Ok(customer);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // /Customer/CreateCustomer
        [HttpPost]
        [Route("CreateCustomer")]
        [IgnoreAntiforgeryToken]
        public IActionResult CreateCustomer([FromForm] Customers model)
        {
            if (ModelState.IsValid)
            {
                _db.Customers.Add(model);
                _db.SaveChanges();
                return Ok("สร้างเรียบร้อย");
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // /Customer/ShowCreateCustomer
        [HttpGet]
        [Route("ShowCreateCustomer")]
        public IActionResult ShowCreateCustomer()
        {
            return View();
        }

        // /Customer/UpdateCustomer/{id}
        [HttpPost]
        [Route("UpdateCustomer/{id}")]
        [IgnoreAntiforgeryToken]
        public IActionResult UpdateCustomer([FromForm] Customers model)
        {
             _db.Customers.Update(model);
             _db.SaveChanges();
             return Ok("แก้ไขเรียบร้อย");
        }

        // /Customer/ShowUpdateCustomer/{id}
        [HttpGet]
        [Route("ShowUpdateCustomer/{id}")]
        public IActionResult ShowUpdateCustomer(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Customers.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        // /Customer/DeleteCustomer/{id}
        [HttpDelete]
        [Route("DeleteCustomer/{id}")]
        [IgnoreAntiforgeryToken]
        public IActionResult DeleteCustomer(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Customers.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Customers.Remove(obj);
            _db.SaveChanges();
            return Ok("ลบเรียบร้อย");
        }


    }
}
