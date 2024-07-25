# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MicroFrontEnd/MicroFrontEnd.csproj", "MicroFrontEnd/"]
RUN dotnet restore "./MicroFrontEnd/MicroFrontEnd.csproj"
COPY . .
WORKDIR "/src/MicroFrontEnd"
RUN dotnet build "./MicroFrontEnd.csproj" -c $BUILD_CONFIGURATION -o /app/build
 
# Stage 2: Publish the application
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MicroFrontEnd.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
 
# Stage 3: Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MicroFrontEnd.dll"]