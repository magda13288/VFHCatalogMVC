# Etap 1: Budowanie aplikacji
FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src

# Skopiuj cały projekt i przywróć zależności
COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

# Etap 2: Obraz runtime
FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "VFHCatalogMVC.Web.dll"]