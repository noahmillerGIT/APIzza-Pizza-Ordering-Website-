using Capstone.DAO;
using Capstone.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Capstone.Controllers
{
    [Route("api/sides")]
    [ApiController]
    public class AddItems : ControllerBase
    {
        private ICreateItems createItemsDao;
        private IUserDao userDao;

        public AddItems(ICreateItems _createItemsDao, IUserDao _userDao)
        {
            this.createItemsDao = _createItemsDao;
            this.userDao = _userDao;
        }

        [HttpGet]
        public ActionResult<List<SideItem>> GetSide()
        {
            return Ok(createItemsDao.GetSideItems());
        }
        [HttpPost]
        public ActionResult<SideItem> AddSide(SideItem sideItem) 
        {
            string username = User.Identity.Name;
            int userId = userDao.GetUser(username).UserId;
            sideItem = createItemsDao.AddSideItem(sideItem, userId);

            return Created("/api/side/" + sideItem.EmployeeId, sideItem);
        }
    }
}
