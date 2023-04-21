using Capstone.DTO;
using Capstone.Models;

namespace Capstone.DAO
{
    public interface ICustomPizza
    {
        CustomOrderDto GetCustomPizzaPickup(CustomOrderDto customPizza);
        CustomOrderDto GetCustomPizzaDeliver(CustomOrderDto customPizza);
        CartDto GetCustomPizza(CartDto cartDto, int orderId);
        CartDto GetOrders(CartDto cartDto);
        CartDto GetAddress(CartDto cartDto);
    }
}
