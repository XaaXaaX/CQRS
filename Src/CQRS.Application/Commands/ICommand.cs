using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Application.Commands
{
    public interface ICommand
    {
        Guid Id { get; }
    }
}
