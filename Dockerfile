# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["./TechChallengeFIAP/TechChallengeFIAP.csproj", "TechChallengeFIAP/"]
COPY ["./TechChallengeFIAP.Application/TechChallengeFIAP.Application.csproj", "TechChallengeFIAP.Application/"]
COPY ["TechChallengeFIAP.Data/TechChallengeFIAP.Data.csproj", "TechChallengeFIAP.Data/"]
COPY ["TechChallengeFIAP.Domain/TechChallengeFIAP.Domain.csproj", "TechChallengeFIAP.Domain/"]
RUN dotnet restore "./TechChallengeFIAP/TechChallengeFIAP.csproj"
COPY . .
WORKDIR "/src/TechChallengeFIAP"
RUN dotnet build "./TechChallengeFIAP.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TechChallengeFIAP.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
ENV CONNECTIONSTRINGSDEFAULTCONNECTION="Server=localhost;Database=FCG;User Id=sa;Password='teste@123';TrustServerCertificate=true;"
ENV JWTSETTINGSSECRET="Yb@7T0m_3Z#jL+eK2w9B9p&gNdT3Ft2D"
ENV JWTSETTINGSISSUER="FiapCloudGames65"
ENV JWTSETTINGSAUDIENCE="FiapCloudGamesApi"
ENV JWTSETTINGSTOKENEXPIRATIONINMINUTES=60
ENV JWTSETTINGSTOKENTIMETOLERANCEINMINUTES=1
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TechChallengeFIAP.dll"]

