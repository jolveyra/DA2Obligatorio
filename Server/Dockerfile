# Usa la imagen oficial de .NET 8 como base para el runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Usa la imagen oficial de .NET SDK 8 para construir la aplicación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ManagementApi/ManagementApi.csproj", "ManagementApi/"]
RUN dotnet restore "ManagementApi/ManagementApi.csproj"
COPY . .
WORKDIR "/src/ManagementApi"
RUN dotnet build "ManagementApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ManagementApi.csproj" -c Release -o /app/publish

# Configura la imagen final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ManagementApi.dll"]