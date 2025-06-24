FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["RetrieverApi/src/Essays.Retriever.Api/Essays.Retriever.Api.csproj", "RetrieverApi/src/Essays.Retriever.Api/"]
COPY ["RetrieverApi/src/Essays.Retriever.Contracts/Essays.Retriever.Contracts.csproj", "RetrieverApi/src/Essays.Retriever.Contracts/"]
COPY ["RetrieverApi/src/Essays.Retriever.Application/Essays.Retriever.Application.csproj", "RetrieverApi/src/Essays.Retriever.Application/"]
COPY ["Core/src/Essays.Core.Models/Essays.Core.Models.csproj", "Core/src/Essays.Models.Data/"]
COPY ["Core/src/Essays.Core.Data/Essays.Core.Data.csproj", "Core/src/Essays.Core.Data/"]
COPY ["Core/src/Essays.Core.Libraries/Essays.Core.Libraries.csproj", "Core/src/Essays.Core.Libraries/"]
RUN dotnet restore "RetrieverApi/src/Essays.Retriever.Api/Essays.Retriever.Api.csproj"
COPY . .
WORKDIR "/src/RetrieverApi/src/Essays.Retriever.Api"
RUN dotnet publish "Essays.Retriever.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
EXPOSE 8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Essays.Retriever.Api.dll"]