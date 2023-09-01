using System.Globalization;

namespace MF.Composition.Routing
{
    public class RoutingMiddleware
    {

        private readonly RequestDelegate _next;

        public RoutingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var routingContext = new RoutingContext();

            if (context.Request.Path == "/")
            {
                routingContext.LayoutName = "home";
            }
            else if (context.Request.Path.StartsWithSegments("/api/myapi"))
            {
                routingContext.ForwardTo = "myapi";
            }

            context.Features.Set(routingContext);

            // Call the next delegate/middleware in the pipeline.
            await _next(context);
        }

    }
}
