using System.Collections.Generic;
using System.Threading.Tasks;
using PizzeriaWebApi.Models;

namespace PizzeriaWebApi.Repositories
{
    public interface IPizzaMongoRepository
    {
        Task<List<PizzaModel>> LoadPizzas();
        PizzaModel LoadPizza(string id);
        void SavePizza(PizzaModel pizza);
    }
}