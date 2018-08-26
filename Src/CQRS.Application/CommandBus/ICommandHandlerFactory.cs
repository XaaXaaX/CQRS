using CQRS.Application.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.Application.CommandBus
{
    public interface ICommandHandlerFactory
    {
        ICommandHandler<T> GetHandler<T>() where T : class, ICommand;
    }
}
