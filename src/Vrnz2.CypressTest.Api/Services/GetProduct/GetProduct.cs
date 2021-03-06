using FluentValidation;
using MediatR;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Vrnz2.BaseContracts.DTOs.Base;
using Vrnz2.CypressTest.Api.Data.Entities;
using Vrnz2.CypressTest.Api.Data.Repositories;

namespace Vrnz2.CypressTest.Api.Services.GetProduct
{
    public class GetProduct
    {
        public class Model
        {
            public class Input
                : BaseDTO.Request, IRequest<Output>
            {
                public Guid ProductId { get; set; }
            }

            public class Output
                : BaseDTO.Response<Product>
            {
            }

            public class RequestValidator
                : AbstractValidator<Input>
            {
                public RequestValidator()
                {
                    RuleFor(v => v)
                        .NotNull()
                        .WithMessage("Request inválida!");
                }
            }
        }

        public class Handler
            : IRequestHandler<Model.Input, Model.Output>
        {
            #region Methods

            public async Task<Model.Output> Handle(Model.Input request, CancellationToken cancellationToken)
                => await Task.FromResult(new Model.Output { Success = true, StatusCode = (int)HttpStatusCode.OK, Content = ProductsRepository.Instance.GetProduct(request.ProductId) });

            #endregion
        }
    }
}
