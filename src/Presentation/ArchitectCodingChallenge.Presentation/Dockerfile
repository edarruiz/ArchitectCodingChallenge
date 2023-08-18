#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Presentation/ArchitectCodingChallenge.Presentation/ArchitectCodingChallenge.Presentation.csproj", "Presentation/ArchitectCodingChallenge.Presentation/"]
COPY ["Application/ArchitectCodingChallenge.Application/ArchitectCodingChallenge.Application.csproj", "Application/ArchitectCodingChallenge.Application/"]
COPY ["Domain/ArchitectCodingChallenge.Domain/ArchitectCodingChallenge.Domain.csproj", "Domain/ArchitectCodingChallenge.Domain/"]
COPY ["Infrastructure/ArchitectCodingChallenge.Infrastructure/ArchitectCodingChallenge.Infrastructure.csproj", "Infrastructure/ArchitectCodingChallenge.Infrastructure/"]
RUN dotnet restore "Presentation/ArchitectCodingChallenge.Presentation/ArchitectCodingChallenge.Presentation.csproj"
COPY . .
WORKDIR "/src/Presentation/ArchitectCodingChallenge.Presentation"
RUN dotnet build "ArchitectCodingChallenge.Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ArchitectCodingChallenge.Presentation.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ArchitectCodingChallenge.Presentation.dll"]