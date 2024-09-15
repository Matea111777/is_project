using EShop.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Service.Interface
{
    public interface IOrderService
    {
        public List<Orders> GetAllOrders();
        public Orders GetDetailsForOrder(BaseEntity model);


    }
}
