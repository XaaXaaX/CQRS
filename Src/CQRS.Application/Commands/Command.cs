using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.Application.Commands
{
    public class Command : ICommand
    {
        public Guid Id { get; set; }

        protected Command()
            :this(Guid.NewGuid())
        {  }

        protected Command(Guid id)
        {
            Id = id;
        }

    }
}
