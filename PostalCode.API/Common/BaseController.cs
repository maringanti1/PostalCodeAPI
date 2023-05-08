using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Logging;

namespace PostalCode.API.Common
{
    public class BaseController : ControllerBase
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        public BaseController(RequestDelegate next, ILogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            if (httpContext.Request.Headers.Keys.Contains("X-Not-Authorized"))
            {
                httpContext.Response.StatusCode = 401;
                return;
            }

            await _next.Invoke(httpContext);
        }

        protected IActionResult HandleException(System.Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(500, "An error occurred, please try again later.");
        }

       
    }
}