#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Identity/Identity.Grpc/Identity.Grpc.csproj", "Services/Identity/Identity.Grpc/"]
COPY ["Services/Identity/Identity.Application/Identity.Application.csproj", "Services/Identity/Identity.Application/"]
COPY ["Services/Identity/Identity.Domain/Identity.Domain.csproj", "Services/Identity/Identity.Domain/"]
COPY ["BuildingBlocks/CommonParts/Services.Common/Services.Common.csproj", "BuildingBlocks/CommonParts/Services.Common/"]
COPY ["BuildingBlocks/Logging/Common.Logging/Common.Logging.csproj", "BuildingBlocks/Logging/Common.Logging/"]
COPY ["Services/Identity/Identity.Infrastructure/Identity.Infrastructure.csproj", "Services/Identity/Identity.Infrastructure/"]
RUN dotnet restore "Services/Identity/Identity.Grpc/Identity.Grpc.csproj"
COPY . .
WORKDIR "/src/Services/Identity/Identity.Grpc"
RUN dotnet build "Identity.Grpc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Identity.Grpc.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Identity.Grpc.dll"]