
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using GMCHPatientImagesDtos.DTOs;
using GMCHPatientImagesFramework.Utils;

namespace GMCHPatientImages.Middlewares
{
   public class ErrorHandlerMiddleware   
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
        await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";


                switch (error)
                {
                    case AppException:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.OK;

                        break;
                    case UnauthorizedException e:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;

                        break;
                    case KeyNotFoundException e:
                        // not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    //case NullReferenceException e:
                    //    // not found error
                    //    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    //    break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
            //Object reference not set to an instance of an object.
                }

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                };

                ReturnObject<bool> result = new ReturnObject<bool>();
                result.Success = false;
                result.Message = error?.Message;
                var res = JsonSerializer.Serialize(result, options);
                await response.WriteAsync(res);
            }
        }
    }
}
