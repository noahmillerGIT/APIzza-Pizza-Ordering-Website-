using Capstone.Models;

namespace Capstone.DAO
{
    public interface ICreateSpecialtyPizaa
    {
        CreateSpecialtyPizza CreateSpecialtyPizza(CreateSpecialtyPizza createSpecialty, int userId);
        bool ChangeAvailability(int createId, bool isAvailable);
        bool EditSpecialtyPizza(int createId, CreateSpecialtyPizza specialtyPizza);

    }
}
