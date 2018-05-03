FROM microsoft/dotnet:2.1-sdk AS mpgpbuild
WORKDIR /app
COPY /. ./
RUN cp -rf ./tools/appsettings.default.json ./tools/appsettings.json
WORKDIR /app/src/Mpgp.RestApiServer
RUN dotnet restore && dotnet publish -c release -o out

FROM microsoft/dotnet:2.1-aspnetcore-runtime AS mpgpweb
WORKDIR /app
RUN apt-get update && apt-get install -y gettext-base
COPY --from=mpgpbuild /app/src/Mpgp.RestApiServer/out ./