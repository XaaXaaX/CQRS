using CQRS.Domain.Orders;
using System;

namespace CQRS.Application.Repository
{
    public interface IRepositoryReadOnly
    {
        Order Find(Guid orderId);
    }
}