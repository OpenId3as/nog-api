using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace OpenId3as.DivulgacaoONGs.Services.Rest.CoreAPI.Controllers
{
    public class OpenAreaControllerBase : Controller
    {
        protected IActionResult OkOrNotFound(object obj)
        {
            if (obj != null)
                return Result(HttpStatusCode.OK, obj);
            else
                return Error(HttpStatusCode.NotFound);
        }

        protected IActionResult Result(HttpStatusCode statusCode, object objectResult = null, string url = null)
        {
            switch (statusCode)
            {
                case HttpStatusCode.OK:
                    return Ok(objectResult);
                case HttpStatusCode.Created:
                    return Created(url, objectResult);
                case HttpStatusCode.NoContent:
                    return NoContent();
                default:
                    return Ok(objectResult);
            }
        }

        protected IActionResult Error(HttpStatusCode statusCode, string errorMessage = "", params string[] errorValues)
        {
            if ((int)statusCode < 400)
                throw new ArgumentException("Status Code must be greater than 399");

            object message = null;
            if (!string.IsNullOrEmpty(errorMessage))
                message = new { error = new { statusCode = (int)statusCode, message = string.Format(errorMessage, errorValues) } };

            switch (statusCode)
            {
                case HttpStatusCode.BadRequest:
                    return BadRequest(message);
                case HttpStatusCode.UnprocessableEntity:
                    return UnprocessableEntity(message);
                case HttpStatusCode.Unauthorized:
                    return Unauthorized();
                case HttpStatusCode.Conflict:
                    return Conflict();
                case HttpStatusCode.NotFound:
                    return NotFound(errorMessage);
                default:
                    return BadRequest();
            }
        }

        protected IActionResult Error(HttpStatusCode statusCode, string name, string errorMessage, string code)
        {
            if ((int)statusCode < 400)
                throw new ArgumentException("Status Code must be greater than 399");

            object message = null;
            if (!string.IsNullOrEmpty(errorMessage))
                message = new { error = new { statusCode = (int)statusCode, message = errorMessage, code, name } };

            switch (statusCode)
            {
                case HttpStatusCode.BadRequest:
                    return BadRequest(message);
                case HttpStatusCode.UnprocessableEntity:
                    return UnprocessableEntity(message);
                case HttpStatusCode.Unauthorized:
                    return Unauthorized();
                default:
                    return BadRequest();
            }
        }
    }
}
