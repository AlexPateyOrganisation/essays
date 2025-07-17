FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["WriterApi/src/Essays.Writer.Api/Essays.Writer.Api.csproj", "WriterApi/src/Essays.Writer.Api/"]
COPY ["WriterApi/src/Essays.Writer.Contracts/Essays.Writer.Contracts.csproj", "WriterApi/src/Essays.Writer.Contracts/"]
COPY ["WriterApi/src/Essays.Writer.Application/Essays.Writer.Application.csproj", "WriterApi/src/Essays.Writer.Application/"]
COPY ["Core/src/Essays.Core.Models/Essays.Core.Models.csproj", "Core/src/Essays.Models.Data/"]
COPY ["Core/src/Essays.Core.Data/Essays.Core.Data.csproj", "Core/src/Essays.Core.Data/"]
COPY ["Core/src/Essays.Core.Libraries/Essays.Core.Libraries.csproj", "Core/src/Essays.Core.Libraries/"]
RUN dotnet restore "WriterApi/src/Essays.Writer.Api/Essays.Writer.Api.csproj"
COPY . .
WORKDIR "/src/WriterApi/src/Essays.Writer.Api"
RUN dotnet publish "Essays.Writer.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
EXPOSE 8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Essays.Writer.Api.dll"]