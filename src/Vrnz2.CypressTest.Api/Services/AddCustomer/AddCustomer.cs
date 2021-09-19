using MediatR;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Vrnz2.BaseContracts.DTOs.Base;
using Vrnz2.CypressTest.Api.Data.Entities;
using Vrnz2.CypressTest.Api.Data.Repositories;
using Vrnz2.Infra.CrossCutting.Types;

namespace Vrnz2.CypressTest.Api.Services.AddCustomer
{
    public class AddCustomer
    {
        public class Model
        {
            public class Input
                : BaseDTO.Request, IRequest<Output>
            {
                public string Name { get; set; }
                public Cpf Cpf { get; set; }
            }

            public class Output
                : BaseDTO.Response<Customer>
            {
            }
        }

        public class Handler
            : IRequestHandler<Model.Input, Model.Output>
        {
            #region Methods

            public async Task<Model.Output> Handle(Model.Input request, CancellationToken cancellationToken)
            {
                var customer = new Customer
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                    Cpf = request.Cpf
                };

                CustomersRepository.Instance.AddCustomer(customer);

                return await Task.FromResult(new Model.Output { Success = true, StatusCode = (int)HttpStatusCode.OK, Content = customer });
            }

            #endregion
        }
    }
}
