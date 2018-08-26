using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.Domain
{
    internal interface IEntity<T> where T: struct , IFormattable
    {
        T Id { get; }
    }
}
