using CQRS.Application.QueriyBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.Application.Queries
{
    public class OrderQuery : IQuery<OrderResult>, IOrderQuery
    {
        public OrderQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
