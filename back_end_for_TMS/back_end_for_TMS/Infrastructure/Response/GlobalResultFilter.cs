using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace back_end_for_TMS.Infrastructure.Response;

public class GlobalResultFilter : IAsyncResultFilter
{
    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        if (context.Result is ObjectResult objectResult && objectResult.StatusCode >= 200 && objectResult.StatusCode < 300)
        {
            var response = new ApiResult<object>
            {
                Instance = context.HttpContext.Request.Path,
                Success = true,
                Status = objectResult.StatusCode ?? 200,
                Data = objectResult.Value,
            };

            context.Result = new ObjectResult(response)
            {
                StatusCode = objectResult.StatusCode
            };
        }

        await next();
    }
}

public class ApiResult<T>
{
    public string? Instance { get; set; }
    public bool Success { get; set; }
    public int Status { get; set; }
    public T? Data { get; set; }
}
