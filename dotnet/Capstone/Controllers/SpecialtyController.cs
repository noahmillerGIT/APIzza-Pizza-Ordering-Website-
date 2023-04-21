using Capstone.DAO;
using Capstone.DTO;
using Capstone.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Capstone.Controllers
{
    [Route("api/specialty")]
    [ApiController]
    public class SpecialtyController : Controller
    {
        public readonly ISpecialtyDAO specialtyDAO;
        public SpecialtyController(ISpecialtyDAO _specialtyDAO)
        {
            this.specialtyDAO = _specialtyDAO;
        }

        [HttpPut]
        public ActionResult<SpecialtyPizza> EditSpecialty(SpecialtyPizza updateSpecialty)
        {
            return EditSpecialty(updateSpecialty);
        }

        //Get the list of all the available Specialty Pizzas
        [HttpGet]
        public ActionResult<List<SpecialtyPizza>> GetAvailableSpecialtyPizza()
        {
            return Ok(specialtyDAO.GetAllAvailableSpecialtyPizza());
        }

        [HttpPost]
        public ActionResult<List<SpecialtyOrderPizzaDto>> GetSpecialtyPizzas([FromBody] SpecialtyOrderPizzaDto specialtyOrderDto)
        
        {
            List<SpecialtyOrderPizzaDto> specialtyOrderPizzas = new List<SpecialtyOrderPizzaDto>();

            try
            {
                // Call the GetSpecialtyPizzasByIds() method to retrieve the specialty pizzas
                List<SpecialtyPizza> specialtyPizzas = specialtyDAO.GetSpecialtyPizzasByIds(specialtyOrderDto.Id);

                foreach (SpecialtyPizza specialtyPizza in specialtyPizzas)
                {
                    if (specialtyOrderDto.CustomerOrder != null)
                    {
                        decimal pizzaPrice = specialtyPizza.Price * specialtyPizza.PizzaQuantity;
                        specialtyOrderDto.CustomerOrder.OrderCost += pizzaPrice;
 
                    }
                }

                specialtyOrderDto.CustomerOrder.Email = specialtyOrderDto.CustomerOrder.Email;

                if (specialtyOrderDto.CustomerOrder.OrderType == "delivery")
                {
                    specialtyDAO.GetSpecialtyOrderDeliver(specialtyOrderDto, specialtyOrderDto.CustomerOrder.OrderCost);
                  //  EmailConfirmation.EmailNotification(specialtyOrderDto);
                }
                else
                {
                    specialtyDAO.GetSpecialtyOrderPickUp(specialtyOrderDto, specialtyOrderDto.CustomerOrder.OrderCost);
                  //  EmailConfirmation.EmailNotification(specialtyOrderDto);
                }

                return specialtyOrderPizzas;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


    }
}
