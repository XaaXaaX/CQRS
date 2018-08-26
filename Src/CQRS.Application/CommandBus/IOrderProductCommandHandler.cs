using CQRS.Application.Commands.Order;
using System.Threading.Tasks;

namespace CQRS.Application.CommandBus
{
    public interface IOrderProductCommandHandler
    {
        Task Invoke(OrderProductCommand command);
    }
}