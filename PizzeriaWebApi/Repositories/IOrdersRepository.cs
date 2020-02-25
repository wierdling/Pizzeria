using PizzeriaWebApi.Models;

namespace PizzeriaWebApi.Repositories
{
    public interface IOrdersRepository
    {
        OrdersModel LoadOrder(string orderId);
        void SaveOrder(OrdersModel order);
        void UpdateOrder(OrdersModel order);
    }
}