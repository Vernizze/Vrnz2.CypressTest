using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using Vrnz2.BaseContracts.DTOs;
using Vrnz2.BaseWebApi.Helpers;

namespace Vrnz2.CypressTest.Api.Controllers
{
    [Route("")]
    public class PingController
        : Controller
    {
        #region Methods

        /// <summary>
        /// Service Heart Beat end point
        /// </summary>
        /// <returns>DateTime.UtcNow + Service Name</returns>
        [HttpGet("ping")]
        [ProducesResponseType(typeof(PingResponse), 200)]
        public async Task<ObjectResult> Ping([FromServices] ControllerHelper controllerHelper)
            => await controllerHelper.ReturnAsync<Ping.Request, Ping.Response, PingResponse>((request) => Task.FromResult(new Ping.Response { Success = true, StatusCode = (int)HttpStatusCode.OK }), new Ping.Request());

        #endregion
    }
}
