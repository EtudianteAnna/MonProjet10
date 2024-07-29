#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MicroFrontEnd/MicroFrontEnd.csproj", "MicroFrontEnd/"]
COPY ["Patient-Services/Patient-Services.csproj", "Patient-Services/"]
RUN dotnet restore "./MicroFrontEnd/MicroFrontEnd.csproj"
COPY . .
WORKDIR "/src/MicroFrontEnd"
RUN dotnet build "./MicroFrontEnd.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MicroFrontEnd.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MicroFrontEnd.dll"]