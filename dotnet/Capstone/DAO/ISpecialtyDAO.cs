using Capstone.DTO;
using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO
{
    public interface ISpecialtyDAO
    {
        IList<SpecialtyPizza> GetAllAvailableSpecialtyPizza();
        SpecialtyOrder GetSpecialtyOrderDeliver(SpecialtyOrderPizzaDto specialtyOrder, decimal orderCost);
        SpecialtyOrderPizzaDto GetSpecialtyOrderPickUp(SpecialtyOrderPizzaDto specialtyOrder, decimal orderCost);
        List<SpecialtyPizza> GetSpecialtyPizzasByIds(List<int> ids);
    }
}
