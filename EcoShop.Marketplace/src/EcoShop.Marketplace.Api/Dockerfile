#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["EcoShop.Marketplace/src/EcoShop.Marketplace.Api/EcoShop.Marketplace.Api.csproj", "EcoShop.Marketplace/src/EcoShop.Marketplace.Api/"]
RUN dotnet restore "EcoShop.Marketplace/src/EcoShop.Marketplace.Api/EcoShop.Marketplace.Api.csproj"
COPY . .
WORKDIR "/src/EcoShop.Marketplace/src/EcoShop.Marketplace.Api"
RUN dotnet build "EcoShop.Marketplace.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EcoShop.Marketplace.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EcoShop.Marketplace.Api.dll"]