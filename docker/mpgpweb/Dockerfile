# FROM microsoft/dotnet:2.2-sdk AS mpgpbuild
FROM microsoft/dotnet:2.2-sdk-alpine AS mpgpbuild
WORKDIR /app
COPY /libs/. ./libs/
COPY /tools/. ./tools/
RUN cp -rf ./tools/appsettings.default.json ./tools/appsettings.json
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
RUN dotnet publish -c release -o out

# FIXME: Use alpine instead:
# FROM microsoft/dotnet:2.2-aspnetcore-runtime-alpine AS mpgpweb
# RUN apk add --no-cache gettext
FROM microsoft/dotnet:2.2-aspnetcore-runtime AS mpgpweb
RUN apt-get update && apt-get install -y gettext-base
WORKDIR /app
COPY --from=mpgpbuild /app/src/Mpgp.RestApiServer/out ./
