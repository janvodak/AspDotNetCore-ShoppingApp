#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /
COPY ["src/ApiGateways/OcelotApiGateway/OcelotApiGateway.csproj", "src/ApiGateways/OcelotApiGateway/"]
COPY ["src/Components/Logger/Logger.csproj", "src/Components/Logger/"]
RUN dotnet restore "src/ApiGateways/OcelotApiGateway/OcelotApiGateway.csproj"
COPY . .
WORKDIR "/src/ApiGateways/OcelotApiGateway"
RUN dotnet build "OcelotApiGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OcelotApiGateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OcelotApiGateway.dll"]
