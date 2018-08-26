using CQRS.Application.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Application.CommandBus
{
    public interface ICommandDispatcher
    {
        /// <summary>
        /// Dispatch the command to the command handler
        /// </summary>
        /// <typeparam name="T">Type of command</typeparam>
        /// <param name="command">Command to execute</param>
        /// <remarks>Implementations should throw exceptions unless they are asynchronous or will attempt to retry later.</remarks>
        Task Dispatch<T>(T command) where T : class, ICommand;
    }
}
