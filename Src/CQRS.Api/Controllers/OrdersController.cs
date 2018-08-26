using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRS.Application;
using CQRS.Application.CommandBus;
using CQRS.Application.Commands.Order;
using CQRS.Application.Queries;
using CQRS.Application.QueriyBus;
using Microsoft.AspNetCore.Mvc;

namespace CQRS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : CQRSControllerBase
    {
        private readonly IQueryDispatcher _qDispatcher;
        protected OrdersController(ICommandDispatcher dispatcher, IQueryDispatcher qDispatcher) : base(dispatcher)
        {
            _qDispatcher = qDispatcher;
        }

        
        [HttpGet]
        public ActionResult<OrderResult> Get(Guid id)
        {
            return _qDispatcher.Execute(new OrderQuery(id));
        }

        
        [HttpPost]
        public async Task Post([FromBody] OrderRequest order)
        {
            await Dispatcher.Dispatch(new OrderProductCommand
            {
                Id = order.Id,
                ProductId = order.ProductId
            });
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
