#FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build-env
#WORKDIR /app
#
## Copy csproj and restore as distinct layers
#COPY src/EcoShop.IdentityService.Api/TestProject.IdentityService.Api.csproj ./
#COPY ["src/TestProject.IdentityService.Application/TestProject.IdentityService.Application.csproj", "../TestProject.IdentityService.Application/"]
#COPY ["src/TestProject.IdentityService.Domain/TestProject.IdentityService.Domain.csproj", "../TestProject.IdentityService.Domain/"]
#COPY ["src/TestProject.IdentityService.Infastructure/TestProject.IdentityService.Infastructure.csproj", "../TestProject.IdentityService.Infastructure/"]
#COPY ["TestProject/TestProject.Common/TestProject.Common.csproj", "../../../../TestProject/TestProject.Common/"]
#RUN dotnet restore 
#
## Copy everything else and build
#COPY . ./
#RUN dotnet publish  -c Release -o out
#
## Build runtime imagey
#FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
#WORKDIR /app
#COPY --from=build-env /app/out .
#ENTRYPOINT ["dotnet", "TestProject.IdentityService.Api.dll"]

#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["TestProject.IdentityService/src/TestProject.IdentityService.Api/TestProject.IdentityService.Api.csproj", "TestProject.IdentityService.Api/"]
COPY ["TestProject.IdentityService/src/TestProject.IdentityService.Application/TestProject.IdentityService.Application.csproj", "TestProject.IdentityService.Application/"]
COPY ["TestProject.IdentityService/src/TestProject.IdentityService.Domain/TestProject.IdentityService.Domain.csproj", "TestProject.IdentityService.Domain/"]
COPY ["TestProject.IdentityService/src/TestProject.IdentityService.Infastructure/TestProject.IdentityService.Infastructure.csproj", "TestProject.IdentityService.Infastructure/"]
COPY ["TestProject.Common/EcoShop.Common.csproj", "TestProject.Common/"]
RUN dotnet restore "TestProject.IdentityService.Api/TestProject.IdentityService.Api.csproj"
COPY . .
WORKDIR "TestProject.IdentityService/src/TestProject.IdentityService.Api"
RUN dotnet build "TestProject.IdentityService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestProject.IdentityService.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestProject.IdentityService.Api.dll"]