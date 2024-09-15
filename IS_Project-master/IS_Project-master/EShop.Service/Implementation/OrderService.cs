using EShop.Domain.Domain;
using EShop.Repository.Implementation;
using EShop.Repository.Interface;
using EShop.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Service.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository returnRepository;

        public OrderService(IOrderRepository returnRepository)
        {
            this.returnRepository = returnRepository;
        }

        public List<Orders> GetAllOrders()
        {
            return returnRepository.GetAllOrders();
        }

        public Orders GetDetailsForOrder(BaseEntity model)
        {
            return returnRepository.GetDetailsForOrder(model);
        }

    }
}
