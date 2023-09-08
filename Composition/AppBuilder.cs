using MF.Composition.Config;
using MF.Composition.Fragments;
using MF.Composition.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Net;

namespace MF.Composition
{
    public static class AppBuilder
    {
        public static WebApplicationBuilder AddFragments(this WebApplicationBuilder builder)
        {
            var services = builder.Services;

            services.AddHttpContextAccessor();
            services.AddHealthChecks();
            services.AddResponseCompression();
            services.AddHttpClient();
            services.AddMemoryCache();

            services.Configure<FragmentsConfig>(builder.Configuration.GetSection("Router"));
            services.Configure<FragmentsConfig>(builder.Configuration.GetSection("Fragments"));

            services.AddTransient<IFragmentProcessor, FragmentProcessor>();
            services.AddTransient<DynamicRouteTransformer>();

            services.AddHttpClient<IFragmentClient, FragmentClient>()
            .SetHandlerLifetime(TimeSpan.FromMinutes(1))
            .ConfigurePrimaryHttpMessageHandler(provider =>
            {
                return new HttpClientHandler
                {
                    AllowAutoRedirect = false,
                    AutomaticDecompression = DecompressionMethods.All,
                    MaxConnectionsPerServer = 1024,
#if DEBUG
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
#endif
                };
            });


            return builder;
        }

    }
}
