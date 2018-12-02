FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY Ullechamp-Api.RestApi/Ullechamp-Api.RestApi.csproj Ullechamp-Api.RestApi/
RUN dotnet restore Ullechamp-Api.RestApi/Ullechamp-Api.RestApi.csproj
COPY . .
WORKDIR /src/Ullechamp-Api.RestApi
RUN dotnet build Ullechamp-Api.RestApi.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Ullechamp-Api.RestApi.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Ullechamp-Api.RestApi.dll"]
