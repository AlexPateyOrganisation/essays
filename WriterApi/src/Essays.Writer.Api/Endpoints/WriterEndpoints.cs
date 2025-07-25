using Essays.Writer.Api.Endpoints.Internal;
using Essays.Writer.Api.Mappings;
using Essays.Writer.Api.Validators;
using Essays.Writer.Application.Repositories;
using Essays.Writer.Application.Repositories.Interfaces;
using Essays.Writer.Application.Services;
using Essays.Writer.Application.Services.Interfaces;
using Essays.Writer.Contracts.Requests;
using Essays.Writer.Contracts.Responses;
using FluentValidation;

namespace Essays.Writer.Api.Endpoints;

public class WriterEndpoints : IEndpoints
{
    private const string ContentType = "application/json";
    private const string BaseRoute = "essays";
    public static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IEssayWriterService, EssayWriterService>();
        services.AddScoped<IEssayCacheService, EssayCacheService>();
        services.AddScoped<IEssayWriterRepository, EssayWriterRepository>();
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<IValidator<EssayRequest>, EssayRequestValidator>();
    }

    public static void DefineEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("/", () => "Writer API")
            .AllowAnonymous();

        app.MapPost(BaseRoute, CreateEssayHandler)
            .WithName("PostEssay")
            .Accepts<EssayRequest>(ContentType)
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);

        app.MapPut($"{BaseRoute}/{{id:guid}}", UpdateEssayHandler)
            .WithName("UpdateEssay")
            .Accepts<EssayRequest>(ContentType)
            .Produces<EssayResponse>()
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest);

        app.MapDelete($"{BaseRoute}/{{id:guid}}", DeleteEssayHandler)
            .WithName("DeleteEssay")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);
    }

    private static async Task<IResult> CreateEssayHandler(EssayRequest essayRequest,
        IEssayWriterService essayWriterService, IValidator<EssayRequest> validator, CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(essayRequest, cancellationToken);

        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var essay = essayRequest.MapToEssay();

        var createdEssay = await essayWriterService.CreateEssay(essay, cancellationToken);

        if (createdEssay is null)
        {
            return Results.BadRequest();
        }

        var essayResponse = createdEssay.MapToEssayResponse();

        return Results.Created($"/essays/{essayResponse.Id}", essayResponse);
    }

    private static async Task<IResult> UpdateEssayHandler(Guid id, EssayRequest essayRequest,
        IEssayWriterService essayWriterService, IValidator<EssayRequest> validator, CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(essayRequest, cancellationToken);

        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var essay = essayRequest.MapToEssay(id);

        var updatedEssay = await essayWriterService.UpdateEssay(essay, cancellationToken);

        if (updatedEssay is null)
        {
            return Results.NotFound();
        }

        var essayResponse = updatedEssay.MapToEssayResponse();

        return Results.Ok(essayResponse);
    }

    private static async Task<IResult> DeleteEssayHandler(Guid id, IEssayWriterService essayWriterService, CancellationToken cancellationToken = default)
    {
        var isDeleted = await essayWriterService.DeleteEssay(id, cancellationToken);

        if (!isDeleted)
        {
            return Results.NotFound();
        }

        return Results.Ok();
    }
}