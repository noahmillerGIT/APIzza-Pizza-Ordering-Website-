using Capstone.DAO;
using Capstone.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Capstone.Controllers
{
    [Route("api/side")]
    [ApiController]
    public class SideController : Controller
    {
        public readonly ISideDAO sideDAO;
        public SideController(ISideDAO _itemDAO)
        {
            this.sideDAO = _itemDAO;
        }
        [HttpGet]
        public List<Item> GetItems()
        {
            return sideDAO.GetItems();
        }
        [HttpPut]
        public Item ToggleAvailability(Item item)
        {
            return sideDAO.ToggleAvailability(item);
        }
    }
}
