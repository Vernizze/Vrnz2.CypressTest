using MediatR;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Vrnz2.BaseContracts.DTOs.Base;
using Vrnz2.CypressTest.Api.Data.Repositories;

namespace Vrnz2.CypressTest.Api.Services.DeleteOrder
{
    public class DeleteOrder
    {
        public class Model
        {
            public class Input
                : BaseDTO.Request, IRequest<Output>
            {
                public Guid OrderId { get; set; }
            }

            public class Output
                : BaseDTO.Response<bool>
            {
            }
        }

        public class Handler
            : IRequestHandler<Model.Input, Model.Output>
        {
            #region Methods

            public async Task<Model.Output> Handle(Model.Input request, CancellationToken cancellationToken)
            {
                OrdersRepository.Instance.DeleteOrder(request.OrderId);

                return await Task.FromResult(new Model.Output { Success = true, StatusCode = (int)HttpStatusCode.OK, Content = true });
            }

            #endregion
        }
    }
}
