using MediatR;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Vrnz2.BaseContracts.DTOs.Base;
using Vrnz2.CypressTest.Api.Data.Entities;
using Vrnz2.CypressTest.Api.Data.Repositories;

namespace Vrnz2.CypressTest.Api.Services.UpdateOrder
{
    public class UpdateOrder
    {
        public class Model
        {
            public class Input
                : BaseDTO.Request, IRequest<Output>
            {
                public Order Order { get; set; }
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
                OrdersRepository.Instance.UpdateOrder(request.Order);

                return await Task.FromResult(new Model.Output { Success = true, StatusCode = (int)HttpStatusCode.OK, Content = request.Order });
            }

            #endregion
        }
    }
}
