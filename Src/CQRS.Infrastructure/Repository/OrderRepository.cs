using CQRS.Application.Repository;
using CQRS.Domain.Orders;
using System;
using System.Threading.Tasks;

namespace CQRS.Infrastructure.Repository
{
    public class OrderRepository : IOrderRepositoryWriteOnly, IRepositoryReadOnly
    {
        public async Task<Domain.Orders.Order> Add(Guid orderid, int productid, DateTime orderdate)
        {
            return new Domain.Orders.Order(orderid, productid);
        }

        public Order Find(Guid orderId)
        {
            return new Domain.Orders.Order(orderId, 1);
        }
    }
}
