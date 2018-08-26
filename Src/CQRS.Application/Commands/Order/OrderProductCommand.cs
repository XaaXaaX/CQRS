using CQRS.Application.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Application.Commands.Order
{
    public class OrderProductCommand : Command, IOrderProductCommand
    {
        public OrderProductCommand()
            : base(Guid.NewGuid())
        { }
        public OrderProductCommand(Guid id)
            : base(id)
        { }

        public int ProductId { get; set; }
    }
}
