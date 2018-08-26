using Microsoft.AspNetCore.Mvc;
using CQRS.Application.CommandBus;
using System;

namespace CQRS.Application
{
    public abstract class CQRSControllerBase : ControllerBase
    {
        protected readonly ICommandDispatcher Dispatcher;
        protected CQRSControllerBase(ICommandDispatcher dispatcher)
            => Dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));
    }
}
