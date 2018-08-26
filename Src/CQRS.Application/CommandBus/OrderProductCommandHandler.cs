using CQRS.Application.Commands.Order;
using CQRS.Application.Repository;
using System;
using System.Threading.Tasks;

namespace CQRS.Application.CommandBus
{

    public class OrderProductCommandHandler : ICommandHandler<OrderProductCommand>, IOrderProductCommandHandler
    {
        private readonly IOrderRepositoryWriteOnly repository;
        public OrderProductCommandHandler(IOrderRepositoryWriteOnly repository)
        {
            this.repository = repository;
        }
        public async Task Invoke(OrderProductCommand command)
        {
            await repository.Add(command.Id, command.ProductId, DateTime.Now);
        }
    }
}
