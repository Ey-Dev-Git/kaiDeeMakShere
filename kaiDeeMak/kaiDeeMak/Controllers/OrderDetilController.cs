using kaiDeeMak.Data;
using kaiDeeMak.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace kaiDeeMak.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderDetilController : Controller
    {

        private readonly ApplicationDBContext _db;
        public OrderDetilController(ApplicationDBContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        // /OrderDetil/AllOrderDetil
        [HttpGet]
        [Route("AllOrderDetil")]
        public IActionResult AllOrderDetil()
        {
            IEnumerable<OrderDetils> allOrderDetil = _db.OrderDetils;

            return Ok(allOrderDetil);
        }

        // /OrderDetil/GetOrderDetilByID/{id}
        [HttpGet]
        [Route("GetOrderDetilByID/{id}")]
        [IgnoreAntiforgeryToken]
        public IActionResult GetOrderDetilByID(int id)
        {
            var order = _db.OrderDetils.Find(id);
            if (order == null || id == 0)
            {
                return NotFound();
            }
            else if (ModelState.IsValid)
            {
                return Ok(order);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // /OrderDetil/CreateOrderDetil
        [HttpPost]
        [Route("CreateOrderDetil")]
        [IgnoreAntiforgeryToken]
        public IActionResult CreateOrderDetil([FromForm] OrderDetils model)
        {
            if (ModelState.IsValid)
            {
                float totalAmount = GetTotalAmount(model);
                model.Total = totalAmount;
                var order = _db.Orders.Find(model.OrderID);
                order.TotalAmount = order.TotalAmount + totalAmount;

                _db.OrderDetils.Add(model);
                _db.SaveChanges();
                return RedirectToAction("ShowCreateOrderDetil");
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // /OrderDetil/ShowCreateOrderDetil
        [HttpGet]
        [Route("ShowCreateOrderDetil")]
        public IActionResult ShowCreateOrderDetil()
        {
            var orderID = (from s in _db.Orders
                              select s.OrderID).ToList();
            var productID = (from s in _db.Products
                             where s.InStock == true
                             select s.ProductID).ToList();

            // กำหนดค่าให้กับ ViewBag
            ViewBag.OrderID = new SelectList(orderID);
            ViewBag.ProductID = new SelectList(productID);
            return View();
        }
        //ddd
        public float GetTotalAmount(OrderDetils modelOrderDetil)
        {
            var orderDetil = modelOrderDetil;
            var productID = orderDetil.ProductID;
            var product = _db.Products.Find(productID);
            float discount = orderDetil.Discount;
            float quantity = orderDetil.Quantity;
            float price = product.Price;
            if (discount <= 1)
            {
                float totalAmount = (price * 1) * quantity;
                return totalAmount;
            }
            else
            {
                float totalAmount = (price * (discount / 100)) * quantity;
                return totalAmount;
            }
        }

        // /OrderDetil/UpdateOrderDetil/{id}
        [HttpPost]
        [Route("UpdateOrderDetil/{id}")]
        [IgnoreAntiforgeryToken]
        public IActionResult UpdateOrderDetil([FromForm] OrderDetils model)
        {
            var orderDetil = _db.OrderDetils.Find(model.OrderDetilID);
            float oldTotal = orderDetil.Total;
            float totalAmount = GetTotalAmount(model);
            orderDetil.Total = totalAmount;

            var order = _db.Orders.Find(model.OrderID);
            order.TotalAmount = order.TotalAmount - oldTotal;
            order.TotalAmount = order.TotalAmount + totalAmount;
           
            _db.SaveChanges();
            return Ok("แก้ไขเรียบร้อย");
        }

        // /OrderDetil/ShowUpdateOrderDetil/{id}
        [HttpGet]
        [Route("ShowUpdateOrderDetil/{id}")]
        public IActionResult ShowUpdateOrderDetil(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.OrderDetils.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            var orderID = (from s in _db.Orders
                           select s.OrderID).ToList();
            var productID = (from s in _db.Products
                             where s.InStock == true
                             select s.ProductID).ToList();

            // กำหนดค่าให้กับ ViewBag
            ViewBag.OrderID = new SelectList(orderID);
            ViewBag.ProductID = new SelectList(productID);
            return View(obj);
        }

        // /OrderDetil/DeleteOrderDetil/{id}
        [HttpDelete()]
        [Route("DeleteOrderDetil/{id}")]
        [IgnoreAntiforgeryToken]
        public IActionResult DeleteOrderDetil(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.OrderDetils.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            var order = _db.Orders.Find(obj.OrderID);
            order.TotalAmount = order.TotalAmount - obj.Total;

            _db.OrderDetils.Remove(obj);
            _db.SaveChanges();
            return Ok("ลบเรียบร้อย");
        }

    }
}
