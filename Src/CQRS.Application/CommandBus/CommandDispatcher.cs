using CQRS.Application.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Application.CommandBus
{
    public sealed class CommandDispatcher : ICommandDispatcher
    {

        private readonly ICommandHandlerFactory _factory;

        public CommandDispatcher(ICommandHandlerFactory factory)
        {
            _factory = factory;
        }

        public async Task Dispatch<T>(T command)
            where T : class, ICommand
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            var handler = _factory.GetHandler<T>();

            if (handler == null)
            {
                throw new CommandHandlerNotFoundException($"command handler {nameof(T)} was not found");
            }

            await handler.Invoke(command);
        }
    }
}

