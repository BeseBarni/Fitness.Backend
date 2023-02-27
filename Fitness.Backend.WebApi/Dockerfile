#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Fitness.Backend.WebApi/Fitness.Backend.WebApi.csproj", "Fitness.Backend.WebApi/"]
COPY ["Fitness.Backend.Application/Fitness.Backend.Application.csproj", "Fitness.Backend.Application/"]
COPY ["Fitness.Backend.Contracts/Fitness.Backend.Application.Contracts.csproj", "Fitness.Backend.Contracts/"]
COPY ["Fitness.Backend.DataContracts/Fitness.Backend.Application.DataContracts.csproj", "Fitness.Backend.DataContracts/"]
COPY ["Fitness.Backend.Domain/Fitness.Backend.Domain.csproj", "Fitness.Backend.Domain/"]
COPY ["Fitness.Backend.Repositories/Fitness.Backend.Repositories.csproj", "Fitness.Backend.Repositories/"]
COPY ["Fitness.Backend.Services/Fitness.Backend.Services.csproj", "Fitness.Backend.Services/"]
RUN dotnet restore "Fitness.Backend.WebApi/Fitness.Backend.WebApi.csproj"
COPY . .
WORKDIR "/src/Fitness.Backend.WebApi"
RUN dotnet build "Fitness.Backend.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Fitness.Backend.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fitness.Backend.WebApi.dll"]