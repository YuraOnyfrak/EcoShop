#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
#
#FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build-env
#WORKDIR /app
#
## Copy csproj and restore as distinct layers
##COPY *.csproj ./
#COPY ["TestProject/EcoShop.ApiGateway.csproj", "EcoShop.ApiGateway.Api/"]
#COPY ["TestProject.Common/EcoShop.Common.csproj", "TestProject.Common/"]
#RUN dotnet restore "EcoShop.ApiGateway.Api/EcoShop.ApiGateway.csproj"
#
## Copy everything else and build
#COPY . ./
#RUN dotnet publish -c Release -o out

# Build runtime image
#FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
#WORKDIR /app
#COPY --from=build-env /app/out .
#ENTRYPOINT ["dotnet", "TestProject.dll"]

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["TestProject/EcoShop.ApiGateway.csproj", "EcoShop.ApiGateway.Api/"]
COPY ["TestProject.Common/EcoShop.Common.csproj", "TestProject.Common/"]
RUN dotnet restore "EcoShop.ApiGateway.Api/EcoShop.ApiGateway.csproj"
COPY . .
WORKDIR "TestProject"
RUN dotnet build "EcoShop.ApiGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EcoShop.ApiGateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EcoShop.ApiGateway.dll"]


