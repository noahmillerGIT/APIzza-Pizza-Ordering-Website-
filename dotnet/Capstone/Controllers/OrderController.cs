using Capstone.DAO;
using Capstone.DTO;
using Capstone.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Capstone.Controllers
{
    [Route("employee/api/order")]
    [ApiController]
    public class OrderController : Controller
    {
        public readonly IOrderDAO orderDAO;
        public readonly IUserDao userDao;
        public readonly IPizzaDAO pizzaDAO;
        public readonly IBeverageDAO beverageDAO;
        public readonly ISideDAO sideDAO;
        

        public OrderController(IOrderDAO _orderDAO, IUserDao _userDao)
        {
            this.orderDAO = _orderDAO;
            this.userDao = _userDao;
        }


        [HttpGet]
        public List<Order> GetAllOrders()
        {
            return orderDAO.GetAllOrders();
        }
        [HttpGet("{id}/summary")]
        public ActionResult<List<string>> OrderSummaryById(int id)
        {
            return null;
        }

        [HttpPut("{id}/status")] //Employee can able to change the status of the orders
        public ActionResult<bool> UpdateOrderStatus(int id, string updateStatus)
        {
            EmailConfirmation email = new EmailConfirmation(orderDAO);
  //          string username = "marshall";
            string username = User.Identity.Name;
            int userId = userDao.GetUser(username).UserId;
            bool updated = orderDAO.UpdateOrderStatus(id, updateStatus, userId );
            email.OrderStatus(id);

            if (updated)
            {
                return updated;
            }
            else
            {
                return BadRequest("failed to update order status");
            }
        }
    }
}
