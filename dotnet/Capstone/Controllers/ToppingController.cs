using Capstone.DAO;
using Capstone.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Capstone.Controllers
{
    [Route("api/topping")]
    [ApiController]
    public class ToppingController : Controller
    {
        public readonly IToppingDAO toppingDAO;
        public ToppingController(IToppingDAO _toppingDAO)
        {
            this.toppingDAO = _toppingDAO;
        }
        [HttpGet]
        public List<Item> GetItems()
        {
            return toppingDAO.GetItems();
        }
        [HttpPut]
        public Item ToggleAvailability(Item item)
        {
            return toppingDAO.ToggleAvailability(item);
        }
    }
}
