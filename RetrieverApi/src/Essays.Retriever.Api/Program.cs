using System;
using Essays.Retriever.Api.Endpoints.Internal;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpoints<Program>(builder.Configuration);

var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("/", () => "Retriever API")
    .AllowAnonymous();

app.UseEndpoints<Program>();

app.Run();

record Essay(Guid Id, string Title, string Content);