FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

ARG BUILD_CONFIGURATION=Release

WORKDIR /src

COPY ["src/SimpleBlocks.Server/SimpleBlocks.Server.csproj", "SimpleBlocks.Server/"]
COPY ["src/SimpleBlocks.Shared/SimpleBlocks.Shared.csproj", "SimpleBlocks.Shared/"]
RUN dotnet restore "SimpleBlocks.Server/SimpleBlocks.Server.csproj"

COPY ["src/SimpleBlocks.Server/", "/src/SimpleBlocks.Server/"]
COPY ["src/SimpleBlocks.Shared/", "/src/SimpleBlocks.Shared/"]

WORKDIR "/src/SimpleBlocks.Server"

RUN dotnet build "SimpleBlocks.Server.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "SimpleBlocks.Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SimpleBlocks.Server.dll"]
