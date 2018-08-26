using CQRS.Application.Commands;
using System.Threading.Tasks;

namespace CQRS.Application.CommandBus
{
    /// <summary>
    /// Handler of a specific command
    /// </summary>
    /// <typeparam name="T">Type of command to handle as input</typeparam>
    /// <remarks>Read the interface name as <c>I Handle Command "TheCommandName"</c></remarks>
    
    public interface ICommandHandler<in T> where T : class, ICommand
    { 
        /// <summary>
        /// Invoke the command
        /// </summary>
        /// <param name="command">Command to run</param>
        Task Invoke(T command);
    }
}
