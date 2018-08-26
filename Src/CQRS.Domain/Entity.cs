using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.Domain
{
    internal class Entity<T> : IEntity<T>
        where T : struct, IFormattable
    {
        public T Id { get; }
    }
}
