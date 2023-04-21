using Capstone.DTO;
using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO
{
    public interface ICartDao
    {
        CartDto GetCustomPizza(CartDto cartDto, int orderId);
        CartDto GetOrders(CartDto cartDto, decimal orderCost);
        CartDto GetAddress(CartDto cartDto);
        List<SpecialtyPizza> GetSpecialtyPizzasByNames(List<string> names);
        List<ItemNames> GetSidesByNames(List<string> names);
        List<ItemNames> GetBeveragesByNames(List<string> names);
    }
}
