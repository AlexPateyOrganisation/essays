using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Essays.Retriever.Api.Endpoints.Internal;

public interface IEndpoints
{
    public static abstract void AddServices(IServiceCollection services, IConfiguration configuration);
    public static abstract void DefineEndpoints(IEndpointRouteBuilder app);
}