FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WriterApi/src/Essays.Writer.Api/Essays.Writer.Api.csproj", "WriterApi/src/Essays.Writer.Api/"]
RUN dotnet restore "WriterApi/src/Essays.Writer.Api/Essays.Writer.Api.csproj"
COPY . .
WORKDIR "/src/WriterApi/src/Essays.Writer.Api"
RUN dotnet build "Essays.Writer.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Essays.Writer.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Essays.Writer.Api.dll"]
