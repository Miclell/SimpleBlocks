FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

ARG BUILD_CONFIGURATION=Release
ARG API_BASE_URL

WORKDIR /src

COPY ["src/SimpleBlocks.Client/SimpleBlocks.Client.csproj", "SimpleBlocks.Client/"]
COPY ["src/SimpleBlocks.Shared/SimpleBlocks.Shared.csproj", "SimpleBlocks.Shared/"]
RUN dotnet restore "SimpleBlocks.Client/SimpleBlocks.Client.csproj"

COPY ["src/SimpleBlocks.Client/", "/src/SimpleBlocks.Client/"]
COPY ["src/SimpleBlocks.Shared/", "/src/SimpleBlocks.Shared/"]

WORKDIR /src/SimpleBlocks.Client

RUN sed -i "s|\"BaseUrl\": \".*\"|\"BaseUrl\": \"${API_BASE_URL}\"|" wwwroot/appsettings.json

RUN dotnet publish "SimpleBlocks.Client.csproj" -c $BUILD_CONFIGURATION -o /app/publish --no-restore

FROM nginx:alpine AS final
WORKDIR /app
EXPOSE 80

COPY ["src/SimpleBlocks.Client/nginx.conf", "/etc/nginx/nginx.conf"]
COPY --from=build /app/publish/wwwroot /usr/share/nginx/html
