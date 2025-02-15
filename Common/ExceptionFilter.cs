namespace BakeMart.Common;

public class ExceptionFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        try
        {
            return await next(context);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Exception caught in Endpoint: {ex.Message}");

            return Results.Problem(
                detail: ex.Message,
                statusCode: StatusCodes.Status500InternalServerError,
                title: "An unexpected error occurred."
            );
        }
    }
}