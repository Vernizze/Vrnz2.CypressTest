using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vrnz2.CypressTest.Api.Data.Entities;
using GetProduct = Vrnz2.CypressTest.Api.Services.GetProduct.GetProduct.Model;
using GetProducts = Vrnz2.CypressTest.Api.Services.GetProducts.GetProducts.Model;
using AddProduct = Vrnz2.CypressTest.Api.Services.AddProduct.AddProduct.Model;
using UpdateProduct = Vrnz2.CypressTest.Api.Services.UpdateProduct.UpdateProduct.Model;
using DeleteProduct = Vrnz2.CypressTest.Api.Services.DeleteProduct.DeleteProduct.Model;
using Vrnz2.BaseContracts.DTOs.Base;
using Vrnz2.BaseWebApi.Helpers;

namespace Vrnz2.CypressTest.Api.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController 
        : ControllerBase
    {
        #region Methods

        [HttpGet("{productId}")]
        //[ProducesResponseType(typeof(GetProduct.Output), 200)]
        public async Task<ObjectResult> GetOne([FromServices] ControllerHelper controllerHelper, [FromServices] IMediator mediator, [FromRoute] Guid productId)
        {
            var request = new GetProduct.Input { ProductId = productId };

            return await controllerHelper.ReturnAsync<GetProduct.Input, GetProduct.Output, Product>((request) => mediator.Send(request), request);
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(BaseDTO.Response<List<Product>>), 200)] 
        public async Task<ObjectResult> GetAll([FromServices] ControllerHelper controllerHelper, [FromServices] IMediator mediator)
        {
            var request = new GetProducts.Input();

            return await controllerHelper.ReturnAsync<GetProducts.Input, GetProducts.Output, List<Product>>((request) => mediator.Send(request), request);
        }

        [HttpPost("")]
        [ProducesResponseType(typeof(BaseDTO.Response<Product>), 200)]
        public async Task<ObjectResult> Add([FromServices] ControllerHelper controllerHelper, [FromServices] IMediator mediator, [FromBody] ProductAddRequest request)
        {
            var addRequest = new AddProduct.Input { Description = request.Description, UnitValue = request.UnitValue };

            return await controllerHelper.ReturnAsync<AddProduct.Input, AddProduct.Output, Product>((request) => mediator.Send(request), addRequest, ignoreMessageCodesFactory: true);
        }

        [HttpPatch("")]
        [ProducesResponseType(typeof(BaseDTO.Response<Product>), 200)]
        public async Task<ObjectResult> Update([FromServices] ControllerHelper controllerHelper, [FromServices] IMediator mediator, [FromBody] ProductUpdateRequest request)
        {
            var updateRequest = new UpdateProduct.Input { Product = request.Product };

            return await controllerHelper.ReturnAsync<UpdateProduct.Input, UpdateProduct.Output, Product>((request) => mediator.Send(request), updateRequest);
        }

        [HttpDelete("{productId}")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<ObjectResult> Update([FromServices] ControllerHelper controllerHelper, [FromServices] IMediator mediator, [FromRoute] Guid productId)
        {
            var request = new DeleteProduct.Input { ProductId = productId };

            return await controllerHelper.ReturnAsync<DeleteProduct.Input, DeleteProduct.Output, bool>((request) => mediator.Send(request), request);
        }

        #endregion
    }

    public class ProductAddRequest
    {
        public string Description { get; set; }
        public decimal UnitValue { get; set; }
    }

    public class ProductUpdateRequest 
    {
        public Product Product { get; set; }
    }
}
