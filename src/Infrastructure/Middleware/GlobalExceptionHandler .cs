using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Serilog;
using Serilog.Context;
using System.Net;

namespace Infrastructure.Middleware;

internal class GlobalExceptionHandler(ISerializerService jsonSerializer) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        string errorId = Guid.NewGuid().ToString();
        LogContext.PushProperty("ErrorId", errorId);
        LogContext.PushProperty("StackTrace", exception.StackTrace);
        var errorResult = new ErrorResult
        {
            Source = exception.TargetSite?.DeclaringType?.FullName,
            Exception = exception.Message.Trim(),
            ErrorId = errorId,
            SupportMessage = "Provide the ErrorId {0} to the support team for further analysis."
        };

        if (exception is not CustomException && exception.InnerException != null)
        {
            while (exception.InnerException != null)
            {
                exception = exception.InnerException;
            }
        }

        if (exception is FluentValidation.ValidationException fluentException)
        {
            errorResult.Exception = "One or More Validations failed.";
            foreach (var error in fluentException.Errors)
            {
                errorResult.Messages.Add(error.ErrorMessage);
            }
        }

        switch (exception)
        {
            case CustomException e:
                errorResult.StatusCode = (int)e.StatusCode;
                if (e.ErrorMessages is not null)
                {
                    errorResult.Messages = e.ErrorMessages;
                }

                break;

            case KeyNotFoundException:
                errorResult.StatusCode = (int)HttpStatusCode.NotFound;
                break;

            case FluentValidation.ValidationException:
                errorResult.StatusCode = (int)HttpStatusCode.BadRequest;
                break;

            default:
                errorResult.StatusCode = (int)HttpStatusCode.InternalServerError;
                break;
        }

        Log.Error($"{errorResult.Exception} Request failed with Status Code {errorResult.StatusCode} and Error Id {errorId}.");
        var response = httpContext.Response;
        if (!response.HasStarted)
        {
            response.ContentType = "application/json";
            response.StatusCode = errorResult.StatusCode;
            await response.WriteAsync(jsonSerializer.Serialize(errorResult), cancellationToken: cancellationToken);
        }
        else
        {
            Log.Warning("Can't write error response. Response has already started.");
        }

        return true;
    }
}