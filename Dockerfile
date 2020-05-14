FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build-env
WORKDIR /app
COPY *.sln ./
COPY src/Bertoni.Core/Bertoni.Core.csproj ./src/Bertoni.Core/
COPY src/Bertoni.Web/Bertoni.Web.csproj ./src/Bertoni.Web/
COPY tests/Bertoni.UnitTests/Bertoni.UnitTests.csproj ./tests/Bertoni.UnitTests/
RUN dotnet restore
COPY . ./
RUN dotnet publish --no-restore -c Release -o WebApp

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0
WORKDIR /app
COPY --from=build-env /app/WebApp .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet Bertoni.Web.dll
