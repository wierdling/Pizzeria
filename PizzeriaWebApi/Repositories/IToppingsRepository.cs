using System.Collections.Generic;
using System.Threading.Tasks;
using PizzeriaWebApi.Models;

namespace PizzeriaWebApi.Repositories
{
    public interface IToppingsRepository
    {
        Task<List<ToppingsModel>> LoadToppings();
        void AddNewToppingType(ToppingsModel topping);
        ToppingsModel GetByName(string toppingName);
        void UpdateToppingQuantity(string toppingName, decimal quantity);
    }
}