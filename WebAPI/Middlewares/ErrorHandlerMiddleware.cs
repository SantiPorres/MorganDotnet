using Domain.CustomExceptions;
using Application.Wrappers;
using System.Net;
using System.Text.Json;

namespace WebAPI.Middlewares
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
            catch (Exception ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = new Response<string>() { Succeeded = false, Message = ex?.Message };

                switch(ex)
                {
                    case BusinessException e:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case FluentValidation.ValidationException e:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        responseModel.Errors = e.Errors.Select(error => error.ErrorMessage).ToList();
                        break;
                    case DataAccessException e:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        if (e.SqlErrors != null)
                        {
                            foreach (var error in e.SqlErrors)
                            {
                                responseModel.Errors.Add((string)error);
                            }
                        }
                        break;
                    case KeyNotFoundException e:
                        response.StatusCode = (int)HttpStatusCode.NotFound; 
                        break;
                    case UnauthorizedAccessException e:
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError; 
                        break;
                }

                var result = JsonSerializer.Serialize(responseModel);
                await response.WriteAsync(result);
            }
        }
    }
}
