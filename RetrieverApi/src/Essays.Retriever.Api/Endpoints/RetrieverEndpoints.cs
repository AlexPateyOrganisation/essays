using System;
using System.Threading.Tasks;
using Essays.Retriever.Api.Endpoints.Internal;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Essays.Retriever.Api.Endpoints;

public class RetrieverEndpoints : IEndpoints
{
    private const string BaseRoute = "essays";
    public static void AddServices(IServiceCollection services, IConfiguration configuration)
    {

    }

    public static void DefineEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet($"{BaseRoute}/{{id:guid}}", GetEssayHandler)
        .WithName("GetEssay")
        .Produces<Essay>()
        .Produces(StatusCodes.Status404NotFound);
    }

    private static async Task<IResult> GetEssayHandler(Guid id)
    {
        return Results.Ok(id);
    }
}