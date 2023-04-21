using Capstone.DAO;
using Capstone.DTO;
using Capstone.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Capstone.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private ICartDao cartDao;

        public CartController(ICartDao _cartDao)
        {
            this.cartDao = _cartDao;
        }

        [HttpPost]
        public ActionResult<CartDto> Card(CartDto cart)
        {

            if (cart.SpecialtyPizzas.Count == 0 && cart.Sides.Count == 0 && cart.Pizzas.Count == 0 && cart.Beverages.Count == 0)
            {
                return NotFound("You don't have any items in the cart");
            }
            if(cart.SpecialtyPizzas.Count > 0)
            {
                List<string> specialtyPizzaNames = new List<string>();

                foreach (SpecialtyPizzaCart items in cart.SpecialtyPizzas)
                {
                    if (items.Name != null)
                    {
                        specialtyPizzaNames.Add(items.Name);
                    }
                }

                List<SpecialtyPizza> specialtyPizzas = cartDao.GetSpecialtyPizzasByNames(specialtyPizzaNames);

                foreach (SpecialtyPizza pizza in specialtyPizzas)
                {
                    if (cart.CustomerOrder != null)
                    {
                        decimal pizzaPrice = pizza.Price * pizza.PizzaQuantity;
                        cart.CustomerOrder.OrderCost += pizzaPrice;
                    }
                }
            }
            if(cart.Sides.Count > 0)
            {
                List<string> itemNames = new List<string>(); 

                foreach (Sides items in cart.Sides)
                {
                    if (items.Name != null)
                    {
                        itemNames.Add(items.Name);
                    }
                }

                List<ItemNames> sidesItems = cartDao.GetSidesByNames(itemNames); 

                foreach (ItemNames items in sidesItems) 
                {
                    if (cart.CustomerOrder != null)
                    {
                        decimal side = (items.Price * cart.Sides.Count);
                        cart.CustomerOrder.OrderCost += side;
                    }
                }
                
            }
            if (cart.Beverages.Count > 0)
            {
                List<string> itemNames = new List<string>();

                foreach (Beverages items in cart.Beverages)
                {
                    if (items.Name != null)
                    {
                        itemNames.Add(items.Name);
                    }
                }

                List<ItemNames> beverageItems = cartDao.GetBeveragesByNames(itemNames);

                foreach (ItemNames items in beverageItems)
                {
                    if (cart.CustomerOrder != null)
                    {
                        decimal beverage = (items.Price * cart.Sides.Count);
                        cart.CustomerOrder.OrderCost += beverage;
                    }
                }

            }

            if (cart.CustomerOrder.OrderType == "delivery")
            {
                cartDao.GetAddress(cart);
            }

            cartDao.GetOrders(cart, cart.CustomerOrder.OrderCost + cart.OrderCost);
           
            EmailConfirmation.EmailNotifications(cart);

            int orderId = cart.CustomerOrder.OrderId; 
            if(cart.Pizzas.Count > 0)
            {
                cartDao.GetCustomPizza(cart, orderId);
            }
            return Created("/api/cart/" + cart.CustomerOrder.OrderId, cart);
        }
    }
}
