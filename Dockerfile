FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build-env
WORKDIR /app
COPY *.sln ./
COPY Bertoni.Core/Bertoni.Core.csproj ./Bertoni.Core/
COPY Bertoni.Web/Bertoni.Web.csproj ./Bertoni.Web/
COPY Bertoni.Tests/Bertoni.Tests.csproj ./Bertoni.Tests/
RUN dotnet restore
COPY . ./
RUN dotnet publish -c Release -o WebApp

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0
WORKDIR /app
COPY --from=build-env /app/WebApp .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet Bertoni.Web.dll