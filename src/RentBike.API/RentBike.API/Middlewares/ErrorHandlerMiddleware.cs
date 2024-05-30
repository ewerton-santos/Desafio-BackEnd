using RentBike.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace RentBike.API.Middlewares
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
            catch (AdminUserNotFoundException exception) { await HandleCustomExceptionAsync(context, exception, HttpStatusCode.Unauthorized); }
            catch (BikeNotAvailableException exception) { await HandleCustomExceptionAsync(context, exception, HttpStatusCode.BadRequest); }
            catch (BikeNotFoundException exception) { await HandleCustomExceptionAsync(context, exception, HttpStatusCode.BadRequest); }
            catch (RemoveBikeException exception) { await HandleCustomExceptionAsync(context, exception, HttpStatusCode.BadRequest); }
            catch (DriverNotQualifiedForCategoryException exception) { await HandleCustomExceptionAsync(context, exception, HttpStatusCode.BadRequest); }
            catch (DriversLicenseNotFoundException exception) { await HandleCustomExceptionAsync(context, exception, HttpStatusCode.NotFound); }
            catch (RentAlreadyCompletedException exception) { await HandleCustomExceptionAsync(context, exception, HttpStatusCode.BadRequest); }
            catch (RentPlanNotFoundExeception exception) { await HandleCustomExceptionAsync(context, exception, HttpStatusCode.NotFound); }
            catch (RentNotFoundExeception exception) { await HandleCustomExceptionAsync(context, exception, HttpStatusCode.NotFound); }
            catch (DeliverymanUserNotFoundException exception) { await HandleCustomExceptionAsync(context, exception, HttpStatusCode.NotFound); }
            catch (DeliverymanCantAcceptOrderException exception) { await HandleCustomExceptionAsync(context, exception, HttpStatusCode.NotAcceptable); }
            catch (DeliverymanWasNotNotifiedException exception) { await HandleCustomExceptionAsync(context, exception, HttpStatusCode.BadRequest); }
            catch (OrderNotAvailableException exception) { await HandleCustomExceptionAsync(context, exception, HttpStatusCode.BadRequest); }
            catch (OrderNotFoundException exception) { await HandleCustomExceptionAsync(context, exception, HttpStatusCode.NotFound); }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("duplicate"))
                    await HandleCustomExceptionAsync(context, new Exception("Duplicate key"), HttpStatusCode.BadRequest);
                else
                    await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;            
            var result = JsonSerializer.Serialize(new { error = exception.Message });
            return context.Response.WriteAsync(result);
        }

        private Task HandleCustomExceptionAsync(HttpContext context, Exception exception, HttpStatusCode httpStatusCode)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)httpStatusCode;
            var result = JsonSerializer.Serialize(new { error = exception.Message });
            return context.Response.WriteAsync(result);
        }

    }

    public static class ExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}
