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
                public Product Product { get; set; }
                public Customer Customer { get; set; }
            }

            public class Output
                : BaseDTO.Response<Order>
            {
            }
        }

        public class Handler
            : IRequestHandler<Model.Input, Model.Output>
        {
            #region Methods

            public async Task<Model.Output> Handle(Model.Input request, CancellationToken cancellationToken)
            {
                var order = new Order
                {
                    Id = Guid.NewGuid(),
                    Number = OrdersRepository.Instance.GetOrdersQtt(),
                    Customer = request.Customer,
                    Product = request.Product,
                    Quantity = request.Quantity
                };

                OrdersRepository.Instance.AddOrder(order);

                return await Task.FromResult(new Model.Output { Success = true, StatusCode = (int)HttpStatusCode.OK, Content = order });
            }

            #endregion
        }
    }
}
