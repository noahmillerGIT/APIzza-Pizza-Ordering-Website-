using Capstone.DAO;
using Capstone.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Capstone.Controllers
{
    [Route("api/pendingorders")]
    [ApiController]
    public class PendingOrdersController : ControllerBase
    {
        private IPendingOrders pendingOrders;

        public PendingOrdersController(IPendingOrders _pendingOrders)
        {
            this.pendingOrders = _pendingOrders;
        }

        [HttpGet]  //Employee can see the list of pending orders
        public ActionResult<IList<PendingOrders>> GetPendingOrders()
      {
           //  username = User.Identity.Name; Commented out so that every employee can see the pending orders
             return Ok(pendingOrders.ViewPendingOrders());
        }
    }
}
