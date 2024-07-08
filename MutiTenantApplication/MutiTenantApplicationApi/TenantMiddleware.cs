namespace MutiTenantApplicationApi
{
    public class TenantMiddleware
    {
        private readonly RequestDelegate _next;

        public TenantMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Assume tenant is identified by a custom header
            var tenantId = context.Request.Headers["Tenant-Id"].FirstOrDefault();

            if (!string.IsNullOrEmpty(tenantId))
            {
                // Set the tenant context (could be stored in HttpContext.Items or a scoped service)
                context.Items["TenantId"] = tenantId;
            }

            await _next(context);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class TenantMiddlewareExtensions
    {
        public static IApplicationBuilder UseTenantMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TenantMiddleware>();
        }
    }
}