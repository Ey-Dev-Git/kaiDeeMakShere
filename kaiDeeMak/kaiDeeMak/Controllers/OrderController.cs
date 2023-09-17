﻿using kaiDeeMak.Data;
using kaiDeeMak.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace kaiDeeMak.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : Controller
    {

        private readonly ApplicationDBContext _db;
        public OrderController(ApplicationDBContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        // /Order/AllOrder
        [HttpGet]
        [Route("AllOrder")]
        public IActionResult AllOrder()
        {
            IEnumerable<Orders> allOrder = _db.Orders;

            return Ok(allOrder);
        }

        // /Order/GetOrderByID/{id}
        [HttpGet]
        [Route("GetOrderByID/{id}")]
        [IgnoreAntiforgeryToken]
        public IActionResult GetOrderByID(int id)
        {
            var order = _db.Orders.Find(id);
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

        // /Order/CreateOrder
        [HttpPost]
        [Route("CreateOrder")]
        [IgnoreAntiforgeryToken]
        public IActionResult CreateOrder([FromForm] Orders model)
        {
            if (ModelState.IsValid)
            {
                float totalAmount = GetTotalAmount(model);
                model.TotalAmount = totalAmount;
                _db.Orders.Add(model);
                _db.SaveChanges();
                return RedirectToAction("ShowCreateOrder");
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // /Order/ShowCreateOrder
        [HttpGet]
        [Route("ShowCreateOrder")]
        public IActionResult ShowCreateOrder()
        {
            var customerID = (from s in _db.Customers
                              select s.CustomerID).ToList();
            var productID = (from s in _db.Products where s.InStock == true
                             select s.ProductID).ToList();

            // กำหนดค่าให้กับ ViewBag
            ViewBag.CustomerID = new SelectList(customerID);
            ViewBag.ProductID = new SelectList(productID);
            return View();
        }
        //ddd
        public float GetTotalAmount(Orders modelOrder) 
        {
            var order = modelOrder;
            var productID = order.ProductID;
            var product = _db.Products.Find(productID);
            float discount = order.Discount;
            float quantity = order.Quantity;
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

        // /Order/UpdateOrder/{id}
        [HttpPost]
        [Route("UpdateOrder/{id}")]
        [IgnoreAntiforgeryToken]
        public IActionResult UpdateOrder([FromForm] Orders model)
        {
            float totalAmount = GetTotalAmount(model);
            model.TotalAmount = totalAmount;
            _db.Orders.Update(model);
            _db.SaveChanges();
            return Ok("แก้ไขเรียบร้อย");
        }

        // /Order/ShowUpdateOrder/{id}
        [HttpGet]
        [Route("ShowUpdateOrder/{id}")]
        public IActionResult ShowUpdateOrder(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Orders.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            var customerID = (from s in _db.Customers
                              select s.CustomerID).ToList();
            var productID = (from s in _db.Products
                             select s.ProductID).ToList();

            // กำหนดค่าให้กับ ViewBag
            ViewBag.CustomerID = new SelectList(customerID);
            ViewBag.ProductID = new SelectList(productID);
            return View(obj);
        }

        // /Order/DeleteOrder/{id}
        [HttpDelete()]
        [Route("DeleteOrder/{id}")]
        [IgnoreAntiforgeryToken]
        public IActionResult DeleteOrder(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Orders.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Orders.Remove(obj);
            _db.SaveChanges();
            return Ok("ลบเรียบร้อย");
        }

    }
}
