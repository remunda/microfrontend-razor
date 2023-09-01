﻿using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;

namespace MF.Composition.Routing
{
    public class DynamicRouteProvider : DynamicRouteValueTransformer
    {
        public override async ValueTask<RouteValueDictionary> TransformAsync(HttpContext httpContext, RouteValueDictionary values)
        {
            var routingContext = httpContext.Features.Get<RoutingContext>();
            if (routingContext?.LayoutName == "home")
            {
                values["page"] = "/Home";
            }

            return values;
        }
    }
}