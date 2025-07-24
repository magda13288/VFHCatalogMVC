# Etap 1: Budowanie aplikacji
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Skopiuj plik projektu i przywróć zależności
COPY VFHCatalogMVC.Web.csproj ./
RUN dotnet restore

# Skopiuj pozostałe pliki i opublikuj projekt
COPY . ./
RUN dotnet publish -c Release -o /app/publish

# Etap 2: Obraz runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "VFHCatalogMVC.Web.dll"]