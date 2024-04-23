// Required namespaces for handling HTTP requests, logging, and custom exceptions.
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Contracts;
using Entities.ErrorModel;
using Entities.Exceptions;

// Define the namespace for the GlobalExceptionHandler to keep the organization of the code.
namespace ReceptionBook;

// GlobalExceptionHandler class implements the IExceptionHandler interface to provide custom exception handling.
public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILoggerManager _logger; // Logger interface to enable logging throughout the class.

    // Constructor to inject a logger instance for logging errors and information.
    public GlobalExceptionHandler(ILoggerManager logger)
    {
        _logger = logger;
    }

    // Method to handle exceptions asynchronously, returns a boolean indicating the handling success.
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext,
        Exception exception, CancellationToken cancellationToken)
    {
        httpContext.Response.ContentType = "application/json"; // Set the response content type to JSON.

        // Retrieve the exception details from the HttpContext.
        var contextFeature = httpContext.Features.Get<IExceptionHandlerFeature>();
        if (contextFeature != null) // Check if the exception details are available.
        {
            // Set HTTP status codes based on the type of exception encountered.
            httpContext.Response.StatusCode = contextFeature.Error switch
            {
                NotFoundException => StatusCodes.Status404NotFound, // Not found exceptions map to HTTP 404.
                BadRequestException => StatusCodes.Status400BadRequest, // Bad request exceptions map to HTTP 400.
                _ => StatusCodes.Status500InternalServerError // All other exceptions map to HTTP 500.
            };

            // Log the error message using the injected logger.
            _logger.LogError($"Something went wrong: {exception.Message}");

            // Write the error details as a JSON response to the client.
            await httpContext.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = httpContext.Response.StatusCode, // Include the status code in the error details.
                Message = contextFeature.Error.Message, // Include the specific error message.
            }.ToString());
        }

        return true; // Return true to indicate that the exception was handled.
    }
}
