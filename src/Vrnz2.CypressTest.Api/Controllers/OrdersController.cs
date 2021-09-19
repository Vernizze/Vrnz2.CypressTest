using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vrnz2.CypressTest.Api.Data.Entities;
using Vrnz2.CypressTest.Api.Helpers;
using GetOrder = Vrnz2.CypressTest.Api.Services.GetOrder.GetOrder.Model;
using GetOrders = Vrnz2.CypressTest.Api.Services.GetOrders.GetOrders.Model;
using AddOrder = Vrnz2.CypressTest.Api.Services.AddOrder.AddOrder.Model;
using UpdateOrder = Vrnz2.CypressTest.Api.Services.UpdateOrder.UpdateOrder.Model;
using DeleteOrder = Vrnz2.CypressTest.Api.Services.DeleteOrder.DeleteOrder.Model;
using Vrnz2.BaseContracts.DTOs.Base;

namespace Vrnz2.CypressTest.Api.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController
        : ControllerBase
    {
        #region Methods

        [HttpGet("{oderId}")]
        public async Task<ObjectResult> GetOne([FromServices] ControllerHelper controllerHelper, [FromServices] IMediator mediator, [FromRoute] Guid orderId)
        {
            var request = new GetOrder.Input { OrderId = orderId };

            return await controllerHelper.ReturnAsync<GetOrder.Input, GetOrder.Output, Order>((request) => mediator.Send(request), request);
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(BaseDTO.Response<List<Order>>), 200)]
        public async Task<ObjectResult> GetAll([FromServices] ControllerHelper controllerHelper, [FromServices] IMediator mediator)
        {
            var request = new GetOrders.Input();

            return await controllerHelper.ReturnAsync<GetOrders.Input, GetOrders.Output, List<Order>>((request) => mediator.Send(request), request);
        }

        [HttpPost("")]
        [ProducesResponseType(typeof(BaseDTO.Response<Order>), 200)]
        public async Task<ObjectResult> Add([FromServices] ControllerHelper controllerHelper, [FromServices] IMediator mediator, [FromBody] OrderAddRequest request)
        {
            var addRequest = new AddOrder.Input 
            {
                Quantity = request.Quantity,
                ProductId = request.ProductId,
                CustomerId = request.CustomerId
            };

            return await controllerHelper.ReturnAsync<AddOrder.Input, AddOrder.Output, Order>((request) => mediator.Send(request), addRequest);
        }

        [HttpPatch("")]
        [ProducesResponseType(typeof(BaseDTO.Response<Order>), 200)]
        public async Task<ObjectResult> Update([FromServices] ControllerHelper controllerHelper, [FromServices] IMediator mediator, [FromBody] OrderUpdateRequest request)
        {
            var updateRequest = new UpdateOrder.Input { Order = request.Order };

            return await controllerHelper.ReturnAsync<UpdateOrder.Input, UpdateOrder.Output, Order>((request) => mediator.Send(request), updateRequest);
        }

        [HttpDelete("{oderId}")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<ObjectResult> Update([FromServices] ControllerHelper controllerHelper, [FromServices] IMediator mediator, [FromRoute] Guid oderId)
        {
            var request = new DeleteOrder.Input { OrderId = oderId };

            return await controllerHelper.ReturnAsync<DeleteOrder.Input, DeleteOrder.Output, bool>((request) => mediator.Send(request), request);
        }

        #endregion
    }

    public class OrderAddRequest
    {
        public int Quantity { get; set; }
        public Guid ProductId { get; set; }
        public Guid CustomerId { get; set; }
    }

    public class OrderUpdateRequest
    {
        public Order Order { get; set; }
    }
}
