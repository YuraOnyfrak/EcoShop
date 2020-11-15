#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["EcoShop.Products/src/EcoShop.Products.Api/EcoShop.Products.Api.csproj", "EcoShop.Products/src/EcoShop.Products.Api/"]
RUN dotnet restore "EcoShop.Products/src/EcoShop.Products.Api/EcoShop.Products.Api.csproj"
COPY . .
WORKDIR "/src/EcoShop.Products/src/EcoShop.Products.Api"
RUN dotnet build "EcoShop.Products.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EcoShop.Products.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EcoShop.Products.Api.dll"]