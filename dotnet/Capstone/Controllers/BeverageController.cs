using Capstone.DAO;
using Capstone.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Capstone.Controllers
{
    [Route("api/beverage")]
    [ApiController]
    public class BeverageController : Controller
    {
        private ICreateItems createItemsDao;
        private IUserDao userDao;
         public readonly IBeverageDAO beverageDAO;

        public BeverageController(ICreateItems _createItemsDao, IUserDao _userDao, IBeverageDAO _beverageDAO) 
        {
            this.createItemsDao = _createItemsDao;
            this.userDao = _userDao;
             this.beverageDAO = _beverageDAO;
        }

        [HttpGet("/get")]
        public ActionResult<List<BeverageItem>> GetBeverage()
        {
            return Ok(createItemsDao.GetBeverageItems());
        }
        [HttpPost]
        public ActionResult<BeverageItem> AddBeverage(BeverageItem beverageItem)
        {
            string username = User.Identity.Name;
            int userId = userDao.GetUser(username).UserId;
            beverageItem = createItemsDao.AddBeverageItem(beverageItem, userId);

            return Created("/api/beverage/" + beverageItem.EmployeeId, beverageItem);
        }
         [HttpGet]
        public List<Item> GetItems()
        {
            return beverageDAO.GetItems();
        }
        [HttpPut]
        public Item ToggleAvailability(Item item)
        {
            return beverageDAO.ToggleAvailability(item);
        }
    }
}
