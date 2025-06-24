FROM mcr.microsoft.com/dotnet/sdk:9.0 AS final
WORKDIR /src
COPY ["Core/src/Essays.Core.Migrations/Essays.Core.Migrations.csproj", "Core/src/Essays.Core.Migrations/"]
COPY ["RetrieverApi/src/Essays.Retriever.Application/Essays.Retriever.Application.csproj", "RetrieverApi/src/Essays.Retriever.Application/"]
RUN dotnet restore "Core/src/Essays.Core.Migrations/Essays.Core.Migrations.csproj"
COPY . .
WORKDIR "/src/Core/src/Essays.Core.Migrations"
RUN dotnet publish "Essays.Core.Migrations.csproj" -c Release -o /app/publish /p:UseAppHost=false
WORKDIR /app/publish
CMD ["dotnet", "Essays.Core.Migrations.dll"]