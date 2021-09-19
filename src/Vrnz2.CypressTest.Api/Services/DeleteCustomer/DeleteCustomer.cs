using MediatR;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Vrnz2.BaseContracts.DTOs.Base;
using Vrnz2.CypressTest.Api.Data.Repositories;

namespace Vrnz2.CypressTest.Api.Services.DeleteCustomer
{
    public class DeleteCustomer
    {
        public class Model
        {
            public class Input
                : BaseDTO.Request, IRequest<Output>
            {
                public Guid CustomerId { get; set; }
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
                CustomersRepository.Instance.DeleteCustomer(request.CustomerId);

                return await Task.FromResult(new Model.Output { Success = true, StatusCode = (int)HttpStatusCode.OK, Content = true });
            }

            #endregion
        }
    }
}
