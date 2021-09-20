using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vrnz2.CypressTest.Api.Data.Entities;
using GetCustomer = Vrnz2.CypressTest.Api.Services.GetCustomer.GetCustomer.Model;
using GetCustomers = Vrnz2.CypressTest.Api.Services.GetCustomers.GetCustomers.Model;
using AddCustomer = Vrnz2.CypressTest.Api.Services.AddCustomer.AddCustomer.Model;
using UpdateCustomer = Vrnz2.CypressTest.Api.Services.UpdateCustomer.UpdateCustomer.Model;
using DeleteCustomer = Vrnz2.CypressTest.Api.Services.DeleteCustomer.DeleteCustomer.Model;
using Vrnz2.BaseContracts.DTOs.Base;
using Vrnz2.BaseWebApi.Helpers;

namespace Vrnz2.CypressTest.Api.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomersController
        : ControllerBase
    {
        #region Methods

        [HttpGet("{customerId}")]
        //[ProducesResponseType(typeof(GetCustomer.Output), 200)]
        public async Task<ObjectResult> GetOne([FromServices] ControllerHelper controllerHelper, [FromServices] IMediator mediator, [FromRoute] Guid customerId)
        {
            var request = new GetCustomer.Input { CustomerId = customerId };

            return await controllerHelper.ReturnAsync<GetCustomer.Input, GetCustomer.Output, Customer>((request) => mediator.Send(request), request);
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(BaseDTO.Response<List<Customer>>), 200)]
        public async Task<ObjectResult> GetAll([FromServices] ControllerHelper controllerHelper, [FromServices] IMediator mediator)
        {
            var request = new GetCustomers.Input();

            return await controllerHelper.ReturnAsync<GetCustomers.Input, GetCustomers.Output, List<Customer>>((request) => mediator.Send(request), request);
        }

        [HttpPost("")]
        [ProducesResponseType(typeof(BaseDTO.Response<Customer>), 200)]
        public async Task<ObjectResult> Add([FromServices] ControllerHelper controllerHelper, [FromServices] IMediator mediator, [FromBody] CustomerAddRequest request)
        {
            var addRequest = new AddCustomer.Input { Name = request.Name, Cpf = request.Cpf };

            return await controllerHelper.ReturnAsync<AddCustomer.Input, AddCustomer.Output, Customer>((request) => mediator.Send(request), addRequest);
        }

        [HttpPatch("")]
        [ProducesResponseType(typeof(BaseDTO.Response<Customer>), 200)]
        public async Task<ObjectResult> Update([FromServices] ControllerHelper controllerHelper, [FromServices] IMediator mediator, [FromBody] CustomerUpdateRequest request)
        {
            var updateRequest = new UpdateCustomer.Input { Customer = request.Customer };

            return await controllerHelper.ReturnAsync<UpdateCustomer.Input, UpdateCustomer.Output, Customer>((request) => mediator.Send(request), updateRequest);
        }

        [HttpDelete("{customerId}")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<ObjectResult> Update([FromServices] ControllerHelper controllerHelper, [FromServices] IMediator mediator, [FromRoute] Guid customerId)
        {
            var request = new DeleteCustomer.Input { CustomerId = customerId };

            return await controllerHelper.ReturnAsync<DeleteCustomer.Input, DeleteCustomer.Output, bool>((request) => mediator.Send(request), request);
        }

        #endregion
    }

    public class CustomerAddRequest
    {
        public string Name { get; set; }
        public string Cpf { get; set; }
    }

    public class CustomerUpdateRequest
    {
        public Customer Customer { get; set; }
    }
}
