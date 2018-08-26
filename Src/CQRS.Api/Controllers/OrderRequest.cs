using System;

namespace CQRS.Api.Controllers
{
    public class OrderRequest
    {
        public Guid Id { get; set; }

        public int ProductId { get; set; }
    }
}