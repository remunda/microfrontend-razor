using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;

namespace MF.Composition.Routing
{
    public class DynamicRouteTransformer : DynamicRouteValueTransformer
    {
        public override async ValueTask<RouteValueDictionary> TransformAsync(HttpContext httpContext, RouteValueDictionary values)
        {
            values ??= new RouteValueDictionary();

            var routingContext = httpContext.Features.Get<RoutingContext>();
            if (routingContext?.LayoutName == "home")
            {
                values["page"] = "/Index";
            }

            return values;
        }
    }
}
