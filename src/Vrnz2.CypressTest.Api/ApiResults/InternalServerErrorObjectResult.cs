﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;

namespace Vrnz2.CypressTest.Api.ApiResults
{
    public class InternalServerErrorObjectResult
        : ObjectResult
    {
        public InternalServerErrorObjectResult(ModelStateDictionary modelState)
            : base(modelState)
        {
            StatusCode = (int)HttpStatusCode.InternalServerError;
        }

        public InternalServerErrorObjectResult(object error)
            : base(error)
        {
            StatusCode = (int)HttpStatusCode.InternalServerError;
        }
    }
}
