﻿using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Vrnz2.BaseContracts.DTOs.Base;
using Vrnz2.CypressTest.Api.ApiResults;
using Vrnz2.Infra.CrossCutting.Extensions;

namespace Vrnz2.CypressTest.Api.Helpers
{
    public class ControllerHelper
    {
        #region Variables

        private readonly ILogger _logger;

        #endregion

        #region Constructors

        public ControllerHelper(ILogger logger)
            => _logger = logger;

        #endregion

        #region Methods

        public async Task<ObjectResult> ReturnAsync<TRequest, TResult, T>(Func<TRequest, Task<TResult>> action, TRequest request, int successStatusCode = (int)HttpStatusCode.OK)
            where TRequest : BaseDTO.Request
            where TResult : BaseDTO.Response<T>
        {
            try
            {
                var response = await action(request);

                switch ((HttpStatusCode)response.StatusCode)
                {
                    case HttpStatusCode statusCode when statusCode.IsSuccessHttpStatusCode():
                        return new OkObjectResult(response) { StatusCode = successStatusCode };
                    case HttpStatusCode.Unauthorized:
                        return new UnauthorizedObjectResult(response);
                    case HttpStatusCode.Forbidden:
                        return new ForbidenObjectResult(response);
                    default:
                        return GetBadRequestObjectResult(new List<string> { response.Message });
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Unexpected error! - Message: {ex.Message}", ex);

                return GetInternalServerErrorObjectResult(new List<string> { $"Unexpected error! - Message: {ex.Message}" });
            }
        }

        private InternalServerErrorObjectResult GetInternalServerErrorObjectResult(List<string> errorCodes)
            => new InternalServerErrorObjectResult(errorCodes);

        private BadRequestObjectResult GetBadRequestObjectResult(List<string> errorCodes)
            => new BadRequestObjectResult(errorCodes);

        #endregion
    }
}
