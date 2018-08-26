using CQRS.Application.QueriyBus;
using CQRS.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.Application.Queries
{
    public class OrderResult : QueryResult<Order>
    {
        public OrderResult(IEnumerable<Order> items, int totalCount) 
            : base(items, totalCount)
        {}
    }
}
