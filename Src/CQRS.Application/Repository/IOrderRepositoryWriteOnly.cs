using System;
using System.Threading.Tasks;

namespace CQRS.Application.Repository
{
    public interface IOrderRepositoryWriteOnly
    {
        Task<Domain.Orders.Order> Add(Guid orderid, int productid, DateTime orderdate);
    }
}