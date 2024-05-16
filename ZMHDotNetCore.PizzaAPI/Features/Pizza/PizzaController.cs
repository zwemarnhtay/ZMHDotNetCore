using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZMHDotNetCore.PizzaAPI.DB;
using ZMHDotNetCore.PizzaAPI.Models;

namespace ZMHDotNetCore.PizzaAPI.Features.Pizza
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private readonly DBContext _dbContext;

        public PizzaController()
        {
            _dbContext = new DBContext();
        }

        [HttpGet]
        public async Task<IActionResult> getPizzas()
         {
            var pizzas = await _dbContext.Pizzas.ToListAsync();
            return Ok(pizzas);
        }

        [HttpGet("Extras")]
        public async Task<IActionResult> getExtrasAsync()
        {
            var lst = await _dbContext.ExtraItems.ToListAsync();
            return Ok(lst);
        }

        [HttpGet("GetOrder/{invoiceNo}")]
        public async Task<IActionResult> getOrder(string invoiceNo)
        {
            var item = await _dbContext.Orders.FirstOrDefaultAsync(x => x.InvoiceNo == invoiceNo);
            var list = await _dbContext.OrderDetail.Where(x => x.InvoiceNo == invoiceNo).ToListAsync();

            return Ok( new
            {
                Order = item,
                Order_Detail = list 
            });
        }

        [HttpPost("Order")]
        public async Task<IActionResult> orderPizza(OrderRequestModel orderReq)
        {
            var orderPizza = _dbContext.Pizzas.FirstOrDefault(x => x.id == orderReq.PizzaId);
            var total = orderPizza.price;

            if(orderReq.Extras.Length > 0)
            {
                var extras = _dbContext.ExtraItems.Where(x => orderReq.Extras.Contains(x.id)).ToList();
                total += extras.Sum(x => x.price);
            }

            var Ino = DateTime.Now.ToString("yyyyMMddHHmmss");

            OrderModel order = new OrderModel()
            {
                InvoiceNo = Ino,
                PizzaId = orderReq.PizzaId,
                TotalAmount = total
            };

            List<OrderDetailModel> orderDetail = orderReq.Extras.Select(x => new OrderDetailModel
            {
                ItemId = x,
                InvoiceNo = Ino,
            }).ToList();

            await _dbContext.Orders.AddAsync(order);
            await _dbContext.OrderDetail.AddRangeAsync(orderDetail);
            await _dbContext.SaveChangesAsync();

            InvoiceModel response = new InvoiceModel()
            {
                InvoiceNo = Ino,
                Message = "Thank you for your order! Enjoy your pizza!",
                TotalAmount = total,
            };

            return Ok(response);
        }
    }
}
