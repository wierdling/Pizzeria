using System.Collections.Generic;
using PizzeriaWebApi.Models;

namespace PizzeriaWebApi.Repositories
{
    public interface IPizzaWebRepository
    {
        List<PizzaModel> LoadPizzas();
        PizzaModel LoadPizza(int id);
        List<ToppingsModel> LoadToppings();
    }
}