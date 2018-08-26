using System.Collections.Generic;

namespace CQRS.Application.QueriyBus
{
    public interface IQueryResult<T> where T : class
    {
        IEnumerable<T> Items { get; }
        int TotalCount { get; }
    }
}