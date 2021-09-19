﻿using MediatR;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Vrnz2.BaseContracts.DTOs.Base;
using Vrnz2.CypressTest.Api.Data.Entities;
using Vrnz2.CypressTest.Api.Data.Repositories;

namespace Vrnz2.CypressTest.Api.Services.GetProducts
{
    public class GetProducts
    {
        public class Model
        {
            public class Input
                : BaseDTO.Request, IRequest<Output>
            {
            }

            public class Output
                : BaseDTO.Response<List<Product>>
            {
            }
        }

        public class Handler
            : IRequestHandler<Model.Input, Model.Output>
        {
            #region Methods

            public async Task<Model.Output> Handle(Model.Input request, CancellationToken cancellationToken)
                => await Task.FromResult(new Model.Output { Success = true, StatusCode = (int)HttpStatusCode.OK, Content = ProductsRepository.Instance.GetProducts() });

            #endregion
        }
    }
}
