#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /
COPY ["src/Services/Order/Order.Rest/Order.Rest.csproj", "src/Services/Order/Order.Rest/"]
COPY ["src/Services/Order/Order.Application/Order.Application.csproj", "src/Services/Order/Order.Application/"]
COPY ["src/Services/Order/Order.Domain/Order.Domain.csproj", "src/Services/Order/Order.Domain/"]
COPY ["src/Services/Order/Order.Infrastructure/Order.Infrastructure.csproj", "src/Services/Order/Order.Infrastructure/"]
COPY ["src/Components/EventBus.Messages/EventBus.Messages.csproj", "src/Components/EventBus.Messages/"]
COPY ["src/Components/Logger/Logger.csproj", "src/Components/Logger/"]
RUN dotnet restore "src/Services/Order/Order.Rest/Order.Rest.csproj"
COPY . .
WORKDIR "/src/Services/Order/Order.Rest"
RUN dotnet build "Order.Rest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Order.Rest.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Order.Rest.dll"]
