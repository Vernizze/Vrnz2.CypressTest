using MediatR;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Vrnz2.BaseContracts.DTOs.Base;
using Vrnz2.CypressTest.Api.Data.Entities;
using Vrnz2.CypressTest.Api.Data.Repositories;

namespace Vrnz2.CypressTest.Api.Services.AddOrder
{
    public class AddOrder
    {
        public class Model
        {
            public class Input
                : BaseDTO.Request, IRequest<Output>
            {
                public int Quantity { get; set; }
                public Guid ProductId { get; set; }
                public Guid CustomerId { get; set; }
            }

            public class Output
                : BaseDTO.Response<Order>
            {
            }
        }

        public class Handler
            : IRequestHandler<Model.Input, Model.Output>
        {
            #region Variables

            private readonly IMediator _mediator;

            #endregion

            #region Constructors

            public Handler(IMediator mediator)
                => _mediator = mediator;

            #endregion

            #region Methods

            public async Task<Model.Output> Handle(Model.Input request, CancellationToken cancellationToken)
            {
                var product = await _mediator.Send(new GetProduct.GetProduct.Model.Input { ProductId = request.ProductId });
                var customer = await _mediator.Send(new GetCustomer.GetCustomer.Model.Input { CustomerId = request.CustomerId });

                var order = new Order
                {
                    Id = Guid.NewGuid(),
                    Number = OrdersRepository.Instance.GetOrdersQtt(),
                    Customer = customer.Content,
                    Product = product.Content,
                    Quantity = request.Quantity
                };

                OrdersRepository.Instance.AddOrder(order);

                return await Task.FromResult(new Model.Output { Success = true, StatusCode = (int)HttpStatusCode.OK, Content = order });
            }

            #endregion
        }
    }
}
