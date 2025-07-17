using Essays.Writer.Api.Endpoints.Internal;
using Essays.Writer.Api.Mappings;
using Essays.Writer.Api.Validators;
using Essays.Writer.Application.Repositories;
using Essays.Writer.Application.Repositories.Interfaces;
using Essays.Writer.Application.Services;
using Essays.Writer.Application.Services.Interfaces;
using Essays.Writer.Contracts.Requests;
using FluentValidation;

namespace Essays.Writer.Api.Endpoints;

public class WriterEndpoints : IEndpoints
{
    private const string ContentType = "application/json";
    private const string BaseRoute = "essays";
    public static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IEssayWriterService, EssayWriterService>();
        services.AddScoped<IEssayWriterRepository, EssayWriterRepository>();
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
    }

    private static async Task<IResult> CreateEssayHandler(EssayRequest essayRequest,
        IEssayWriterService essayWriterService, IValidator<EssayRequest> validator)
    {
        var validationResult = await validator.ValidateAsync(essayRequest);

        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var essay = essayRequest.MapToEssay();

        var isCreated = await essayWriterService.CreateEssay(essay);

        if (!isCreated)
        {
            return Results.BadRequest();
        }

        var essayResponse = essay.MapToEssayResponse();

        return Results.Created($"/essays/{essayResponse.Id}", essayResponse);
    }
}