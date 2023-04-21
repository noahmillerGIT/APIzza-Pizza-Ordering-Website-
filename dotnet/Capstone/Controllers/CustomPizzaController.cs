using Capstone.DAO;
using Capstone.DTO;
using Capstone.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace Capstone.Controllers
{
    [Route("api/custompizza")]
    [ApiController]
    public class CustomPizzaController : ControllerBase
    {
        private ICustomPizza customPizzaDao;
        private IPendingOrders pendingOrders;

        public CustomPizzaController(ICustomPizza _customPizza, IPendingOrders _pendingOrders)
        {
            customPizzaDao = _customPizza;
            pendingOrders = _pendingOrders;
        }

        [HttpPost]
        public ActionResult<CustomOrderDto> GetCustomPizza(CustomOrderDto customPizza)
        {


            if (customPizza.CustomerOrder.OrderType == "pickup")
            {
                customPizza = customPizzaDao.GetCustomPizzaPickup(customPizza);
             //   EmailConfirmation.EmailNotifications(customPizza);
            }
            else if (customPizza.CustomerOrder.OrderType == "delivery")
            {
                customPizza = customPizzaDao.GetCustomPizzaDeliver(customPizza);
             //   EmailConfirmation.EmailNotifications(customPizza);
            }

            return Created("/api/custompizza/" + customPizza.CustomerOrder.OrderId, customPizza);
        }

        [HttpGet]
        public ActionResult<PendingOrders> GetPizzaById(int id)
        {
            return Ok(pendingOrders.GetPizzaById(id));
        }

    }
}
