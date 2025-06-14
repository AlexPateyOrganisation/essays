using Essays.Retriever.Api.Endpoints.Internal;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpoints<Program>(builder.Configuration);

var app = builder.Build();

app.UseHttpsRedirection();
app.UseEndpoints<Program>();

app.Run();