#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["WebApps/Shopping.WebApp/Shopping.WebApp.csproj", "WebApps/Shopping.WebApp/"]
RUN dotnet restore "WebApps/Shopping.WebApp/Shopping.WebApp.csproj"
COPY . .
WORKDIR "/src/WebApps/Shopping.WebApp"
RUN dotnet build "Shopping.WebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Shopping.WebApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shopping.WebApp.dll"]
