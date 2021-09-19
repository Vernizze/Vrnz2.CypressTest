using MediatR;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Vrnz2.BaseContracts.DTOs.Base;
using Vrnz2.CypressTest.Api.Data.Entities;
using Vrnz2.CypressTest.Api.Data.Repositories;

namespace Vrnz2.CypressTest.Api.Services.AddProduct
{
    public class AddProduct
    {
        public class Model
        {
            public class Input
                : BaseDTO.Request, IRequest<Output>
            {
                public string Description { get; set; }
                public decimal UnitValue { get; set; }
            }

            public class Output
                : BaseDTO.Response<Product>
            {
            }
        }

        public class Handler
            : IRequestHandler<Model.Input, Model.Output>
        {
            #region Methods

            public async Task<Model.Output> Handle(Model.Input request, CancellationToken cancellationToken)
            {
                var product = new Product
                {
                    Id = Guid.NewGuid(),
                    Description = request.Description,
                    UnitValue = request.UnitValue
                };

                ProductsRepository.Instance.AddProduct(product);

                return await Task.FromResult(new Model.Output { Success = true, StatusCode = (int)HttpStatusCode.OK, Content = product });
            }

            #endregion
        }
    }
}
