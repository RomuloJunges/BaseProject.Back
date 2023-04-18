using BaseProject.Domain.Models;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;

namespace BaseProject.Service.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;

            if (exception is ArgumentException || exception is ValidationException)
            {
                statusCode = HttpStatusCode.BadRequest;
            }
            else if (exception is ResponseException respostaEx)
            {
                statusCode = respostaEx.Status;
            }
            else
            {
                _logger.LogError(exception, exception.Message);
            }

            if (!httpContext.Response.HasStarted)
            {
                ErrorApi errorApi = new();
                if (exception is ValidationException exceptionValidation)
                {
                    List<string> erros = new();
                    foreach (ValidationFailure falhaValidacao in exceptionValidation.Errors)
                    {
                        erros.Add($"{falhaValidacao.PropertyName}: {falhaValidacao.ErrorMessage}");
                    }

                    errorApi.Message = "Fail on data validation.";
                    errorApi.Erros = erros;
                }
                else
                {
                    errorApi.Message = exception.Message;
                }

                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)statusCode;

                JsonSerializerOptions serializerOptions = new()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                };

                return httpContext.Response.WriteAsync(JsonSerializer.Serialize(errorApi, serializerOptions));
            }

            return Task.Factory.StartNew(() => { });
        }

    }
}
