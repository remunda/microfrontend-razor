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
                routingContext.LayoutName = "Home";
            }
            else if (context.Request.Path == "/content")
            {
                routingContext.LayoutName = "Content";
            }
            else if (context.Request.Path.StartsWithSegments("/api/myapi"))
            {
                // TBD: forward calls to APIs
                routingContext.ForwardTo = "myapi";
            }

            context.Features.Set(routingContext);

            // Call the next delegate/middleware in the pipeline.
            await _next(context);
        }

    }
}
