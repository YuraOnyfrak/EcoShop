#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["EcoShop.Entrepreneur/src/EcoShop.Entrepreneur.Api/EcoShop.Entrepreneur.Api.csproj", "EcoShop.Entrepreneur.Api/"]
COPY ["EcoShop.Entrepreneur/src/EcoShop.Entrepreneur.Application/EcoShop.Entrepreneur.Application.csproj", "EcoShop.Entrepreneur.Application/"]
COPY ["EcoShop.Entrepreneur/src/EcoShop.Entrepreneur.Domain/EcoShop.Entrepreneur.Domain.csproj", "EcoShop.Entrepreneur.Domain/"]
COPY ["EcoShop.Entrepreneur/src/EcoShop.Entrepreneur.Infrastructure/EcoShop.Entrepreneur.Infrastructure.csproj", "EcoShop.Entrepreneur.Infrastructure/"]
COPY ["TestProject.Common/EcoShop.Common.csproj", "TestProject.Common/"]
RUN dotnet restore "EcoShop.Entrepreneur.Api/EcoShop.Entrepreneur.Api.csproj"
COPY . .
WORKDIR "EcoShop.Entrepreneur/src/EcoShop.Entrepreneur.Api"
RUN dotnet build "EcoShop.Entrepreneur.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EcoShop.Entrepreneur.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EcoShop.Entrepreneur.Api.dll"]