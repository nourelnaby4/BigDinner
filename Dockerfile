# Use the .NET SDK image for building the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
# Copy the csproj files and restore dependencies
COPY ["BigDinner.API/BigDinner.API.csproj", "BigDinner.API/"]
COPY ["BigDinner.Application/BigDinner.Application.csproj", "BigDinner.Application/"]
COPY ["BigDinner.Domain/BigDinner.Domain.csproj", "BigDinner.Domain/"]
COPY ["BigDinner.Persistence/BigDinner.Persistence.csproj", "BigDinner.Persistence/"]
COPY ["BigDinner.Service/BigDinner.Service.csproj", "BigDinner.Service/"]

RUN dotnet restore "BigDinner.API/BigDinner.API.csproj"

# Copy the entire source code and build the application
COPY . .
WORKDIR /src/BigDinner.API
RUN dotnet build "BigDinner.API.csproj" -c Release -o /app/build

# Use the ASP.NET Core runtime image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/build .
ENTRYPOINT ["dotnet", "BigDinner.API.dll"]
