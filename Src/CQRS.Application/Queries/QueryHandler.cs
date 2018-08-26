using CQRS.Application.QueriyBus;
using CQRS.Application.Repository;
using CQRS.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.Application.Queries
{
    public class OrderHandler : IExecuteQuery<OrderQuery, OrderResult>, IOrderHandler
    {
        private readonly IRepositoryReadOnly respositoryReadOnly;
        public OrderHandler(IRepositoryReadOnly repository)
        {
            respositoryReadOnly = repository;
        }

        public OrderResult Execute(OrderQuery query)
        {
            var ret = respositoryReadOnly.Find(query.Id);
            List<Order> itm = new List<Order>();
            itm.Add(ret);
            OrderResult res = new OrderResult(itm, 1);

            return res;
        }
    }
}
