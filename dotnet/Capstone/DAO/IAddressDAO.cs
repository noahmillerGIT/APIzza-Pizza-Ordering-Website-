using Capstone.Models;

namespace Capstone.DAO
{
    public interface IAddressDAO
    {
        Address GetAddress(string addressID);

        Address AddAddress(Address addMe);
        Address EditAddress(Address update);
    }
}
