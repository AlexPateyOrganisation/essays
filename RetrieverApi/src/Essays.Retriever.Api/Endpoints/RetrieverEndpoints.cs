using Essays.Retriever.Api.Endpoints.Internal;
using Essays.Retriever.Api.Mappings;
using Essays.Retriever.Application.Repositories;
using Essays.Retriever.Application.Repositories.Interfaces;
using Essays.Retriever.Application.Services;
using Essays.Retriever.Application.Services.Interfaces;
using Essays.Retriever.Contracts.Responses;

namespace Essays.Retriever.Api.Endpoints;

public class RetrieverEndpoints : IEndpoints
{
    private const string BaseRoute = "essays";
    public static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IEssayRetrieverService, EssayRetrieverService>();
        services.AddScoped<IEssayCacheService, EssayCacheService>();
        services.AddScoped<IEssayRetrieverRepository, EssayRetrieverRepository>();
    }

    public static void DefineEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("/", () => "Retriever API")
            .AllowAnonymous();

        app.MapGet($"{BaseRoute}/{{id:guid}}", GetEssayHandler)
        .WithName("GetEssay")
        .Produces<EssayResponse>()
        .Produces(StatusCodes.Status404NotFound);
    }

    private static async Task<IResult> GetEssayHandler(Guid id, IEssayRetrieverService essayRetrieverService, CancellationToken cancellationToken = default)
    {
        var essay = await essayRetrieverService.GetEssay(id, cancellationToken);

        if (essay is null)
        {
            return Results.NotFound();
        }

        var essayResponse = essay.MapToEssayResponse();
        return Results.Ok(essayResponse);
    }
}