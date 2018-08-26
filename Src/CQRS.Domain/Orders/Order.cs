using System;

namespace CQRS.Domain.Orders
{
    public class Order : IEntity<Guid>
    {
        public Order(Guid id, int productId)
        {
            Id = id;
            ProductId = productId;
        }


        public Guid Id { get; }

        public int ProductId { get; set; }

    }
}
