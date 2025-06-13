using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Essays.Retriever.Api.Endpoints.Internal;

public static class EndpointsExtensions
{
    public static void AddEndpoints<TMarker>(this IServiceCollection services, IConfiguration configuration)
    {
        var endpointTypes = GetEndpointTypesFromAssemblyContaining(typeof(TMarker));

        foreach (var endpointType in endpointTypes)
        {
            endpointType.GetMethod(nameof(IEndpoints.AddServices))!
                .Invoke(null, [services, configuration]);
        }
    }

    public static void UseEndpoints<TMarker>(this IApplicationBuilder app)
    {
        var endpointTypes = GetEndpointTypesFromAssemblyContaining(typeof(TMarker));

        foreach (var endpointType in endpointTypes)
        {
            endpointType.GetMethod(nameof(IEndpoints.DefineEndpoints))!
                .Invoke(null, [app]);
        }
    }

    private static IEnumerable<TypeInfo> GetEndpointTypesFromAssemblyContaining(Type typeMarker)
    {
        var endpointTypes =
            typeMarker.Assembly.DefinedTypes
                .Where(ti => ti is { IsAbstract: false, IsInterface: false } && typeof(IEndpoints).IsAssignableFrom(ti));
        return endpointTypes;
    }
}