using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;

namespace MF.Composition.Routing
{
    /// <summary>
    /// Provides an abstraction for dynamically manipulating route value to select a controller action or page.
    /// </summary>
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
            else if (routingContext?.LayoutName == "content")
            {
                values["page"] = "/Content";
            }

            return values;
        }
    }
}
