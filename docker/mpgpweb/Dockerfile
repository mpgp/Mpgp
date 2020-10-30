FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS mpgpbuild
WORKDIR /app
COPY /libs/. ./libs/
COPY /tools/. ./tools/
COPY /tools/appsettings.prod.json ./tools/appsettings.json
COPY /src/Mpgp.sln ./src/Mpgp.sln
COPY /src/Mpgp.Abstract/Mpgp.Abstract.csproj ./src/Mpgp.Abstract/Mpgp.Abstract.csproj
COPY /src/Mpgp.DataAccess/Mpgp.DataAccess.csproj ./src/Mpgp.DataAccess/Mpgp.DataAccess.csproj
COPY /src/Mpgp.Infrastructure/Mpgp.Infrastructure.csproj ./src/Mpgp.Infrastructure/Mpgp.Infrastructure.csproj
COPY /src/Mpgp.Shared/Mpgp.Shared.csproj ./src/Mpgp.Shared/Mpgp.Shared.csproj
COPY /src/Mpgp.Domain/Mpgp.Domain.csproj ./src/Mpgp.Domain/Mpgp.Domain.csproj
COPY /src/Mpgp.RestApiServer/Mpgp.RestApiServer.csproj ./src/Mpgp.RestApiServer/Mpgp.RestApiServer.csproj
COPY /src/Mpgp.WebSocketServer/Mpgp.WebSocketServer.csproj ./src/Mpgp.WebSocketServer/Mpgp.WebSocketServer.csproj
WORKDIR /app/src/Mpgp.RestApiServer
RUN dotnet restore
COPY /src/. /app/src/
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS mpgpweb
RUN apt-get update && apt-get install -y gettext-base
WORKDIR /app
COPY --from=mpgpbuild /app/src/Mpgp.RestApiServer/out ./
