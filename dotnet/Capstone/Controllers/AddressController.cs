using Capstone.DAO;
using Capstone.Models;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Controllers
{
    [Route("api/user/address")]
    public class AddressController : Controller
    {
        public readonly IAddressDAO addressDAO;
        public AddressController(IAddressDAO _addressDAO)
        {
            this.addressDAO = _addressDAO;
        }

        [HttpGet]
        public ActionResult<Address> GetAddress(string addressId)
        {
            Address result = addressDAO.GetAddress(addressId);
            return result;
        }
        [HttpPost]
        public ActionResult<Address> AddAddress(Address addMe)
        {
            return addressDAO.AddAddress(addMe);
        }
        [HttpPut]
        public ActionResult<Address> EditAddress(Address update)
        {
            return addressDAO.EditAddress(update);
        }
    }
}
