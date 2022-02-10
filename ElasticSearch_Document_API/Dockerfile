#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 5003

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ElasticSearch_Document_API/ElasticSearch_Document_API.csproj", "ElasticSearch_Document_API/"]
RUN dotnet restore "ElasticSearch_Document_API/ElasticSearch_Document_API.csproj"
COPY . .
WORKDIR "/src/ElasticSearch_Document_API"
RUN dotnet build "ElasticSearch_Document_API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ElasticSearch_Document_API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ElasticSearch_Document_API.dll"]