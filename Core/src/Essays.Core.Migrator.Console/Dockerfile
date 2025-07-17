FROM mcr.microsoft.com/dotnet/sdk:9.0 AS final
WORKDIR /src
COPY ["Core/src/Essays.Core.Migrator.Console/Essays.Core.Migrator.Console.csproj", "Core/src/Essays.Core.Migrator.Console/"]
COPY ["RetrieverApi/src/Essays.Retriever.Application/Essays.Retriever.Application.csproj", "RetrieverApi/src/Essays.Retriever.Application/"]
RUN dotnet restore "Core/src/Essays.Core.Migrator.Console/Essays.Core.Migrator.Console.csproj"
COPY . .
WORKDIR "/src/Core/src/Essays.Core.Migrator.Console"
RUN dotnet publish "Essays.Core.Migrator.Console.csproj" -c Release -o /app/publish /p:UseAppHost=false
WORKDIR /app/publish
CMD ["dotnet", "Essays.Core.Migrator.Console.dll"]