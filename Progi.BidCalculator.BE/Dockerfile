# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files and restore dependencies
COPY ["Progi.BidCalculator.API/Progi.BidCalculator.API.csproj", "Progi.BidCalculator.API/"]
COPY ["Progi.BidCalculator.Application/Progi.BidCalculator.Application.csproj", "Progi.BidCalculator.Application/"]
COPY ["Progi.BidCalculator.Domain/Progi.BidCalculator.Domain.csproj", "Progi.BidCalculator.Domain/"]
COPY ["Progi.BidCalculator.Infrastructure/Progi.BidCalculator.Infrastructure.csproj", "Progi.BidCalculator.Infrastructure/"]

RUN dotnet restore "Progi.BidCalculator.API/Progi.BidCalculator.API.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/src/Progi.BidCalculator.API"
RUN dotnet build "Progi.BidCalculator.API.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "Progi.BidCalculator.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

# Install curl for health checks
RUN apt-get update && apt-get install -y curl && rm -rf /var/lib/apt/lists/*

WORKDIR /app
EXPOSE 8080
EXPOSE 8081

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Progi.BidCalculator.API.dll"]

