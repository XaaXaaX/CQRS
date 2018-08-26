using System;

namespace CQRS.Application.Queries
{
    public interface IOrderQuery
    {
        Guid Id { get; set; }
    }
}