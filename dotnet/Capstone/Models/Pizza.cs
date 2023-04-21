using System.Collections.Generic;
using System.Linq;

namespace Capstone.Models
{
    public class Pizza
    {
        public int PizzaId { get; set; }
        public PizzaSize Size { get; set; }
        public List<PizzaTopping> Toppings { get; set; }
        public decimal _Price { get; set; }
        public decimal Price {
            get
            {
                if(_Price == 0)
                {
                    decimal totalPizzaPrice = (Size.Price + Toppings.Count * 1.5M) * PizzaQuantity;
                    return totalPizzaPrice;

                }
                else
                {
                    return _Price;
                }
            }
        }
        public PizzaCrusts Crusts { get; set; }
        public PizzaSauces Sauces { get; set; }
        public int PizzaQuantity { get; set; } = 1;
    }

    public class PizzaSize
    {
        public PizzaOptions.SizeOption Size { get; set; }
        public decimal Price
        {
            get
            {
                switch (Size)
                {
                    case PizzaOptions.SizeOption.Small:
                        return 8.99M;
                    case PizzaOptions.SizeOption.Medium:
                        return 10.99M;
                    case PizzaOptions.SizeOption.Large:
                        return 12.99M;
                    case PizzaOptions.SizeOption.ExtraLarge:
                        return 15.99M;
                    default:
                        return 0M;
                }
            }
        }

        public PizzaSize() { }
        public PizzaSize(PizzaOptions.SizeOption size)
        {
            Size = size;

            
        }
    }

    public class PizzaTopping
    {
        public PizzaOptions.ToppingsOption Topping { get; set; }
        public decimal Price 
        { get 
            { 
                return 1.50M; 
            } 
        }
    }

    public class PizzaCrusts
    {
        public PizzaOptions.CrustsOptions Crusts { get; set; }
    }

    public class PizzaSauces
    {
        public PizzaOptions.SaucesOptions Sauces { get; set; }
    }

    public class PizzaOptions
    {
        public enum SizeOption
        {
            Small,
            Medium,
            Large,
            ExtraLarge
        }

        public enum ToppingsOption
        {
            Pepperoni,
            Mushroom,
            Ham,
            Bacon,
            Sausage,
            ExtraCheese,
            Pineapple,
            GoldFlake,
            Onion,
            Spinach,
            Garlic,
            Tomato,
            Green_Peppers,
            Banana_Peppers,
            Steak,
            Chicken,

        }

        public enum CrustsOptions
        {
            HandCrust,
            ThinCrust
        }

        public enum SaucesOptions
        {
            Red,
            White,
            Azure
        }
    }
}
